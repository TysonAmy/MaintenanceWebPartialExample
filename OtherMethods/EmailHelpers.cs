using MaintenanceLibrary.Models;
using MaintenanceWebsite.Controllers;
using MaintenanceWebsite.Models;
using MaintenanceWebsite.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MaintenanceWebsite.OtherMethods
{
    public class EmailHelpers
    {
        /// <summary>
        /// Creates an email to display downtime issues for the last 12 hours.
        /// Beware: This function is saved in Hangfire so that it can be run later.
        /// </summary>
        /// <param name="emailSender"> used to send the email</param>
        /// <returns>A <see cref="Task"/> that sends the email</returns>
        public static async Task SendEmailAboutDowntimeIssue12hours(EmailSender emailSender)
        {
            try
            {
                var downtimeIssues = MaintenanceLibrary.BusinessLogic.DowntimeIssuesProcessor.GetDowntimeIssuesBy_StartDate_EndDate(DateTime.Now.AddDays(-1), DateTime.Now);

                string subject;
                string body;


                subject = "TEST Maintenance Application TEST 12 Hour Downtime Issue Review";
                body = "";
                body += "<H1>TEST OF MAINTENANCE WEB APPLICATION</H1>";
                body += "<A href='https://kkpwrwcip01.tyson.com/'>Link to Maintenance Website</A><BR>";
                body += "<H1>Downtime Issues for last 12 hours</H1>";
                body += "<BR>";
                body += "<TABLE  border='1'>";
                body += "<TR>";
                body += "   <TH>Date Created</TH>";
                body += "   <TH>Supervisor</TH>";
                body += "   <TH>Area</TH>";
                body += "   <TH>Equipment</TH>";
                body += "   <TH>Issue Resolution</TH>";
                body += "   <TH>Downtime</TH>";
                body += "</TR>";
                foreach (var downtimeIssue in downtimeIssues)
                {
                    body += "<TR>";
                    body += $"  <TD> {downtimeIssue.Created}</TD>";
                    body += $"  <TD> {downtimeIssue.Employee.FullName}</TD>";
                    body += $"  <TD> {downtimeIssue.Equipment.Area.Name}</TD>";
                    body += $"  <TD> {downtimeIssue.Equipment.Name}</TD>";
                    body += $"  <TD> {downtimeIssue.IssueResolution}</TD>";
                    body += $"  <TD> {downtimeIssue.DownTime}</TD>";
                    body += "</TR>";
                }
                body += "</TABLE>";

                await emailSender.SendEmailAsync("dl-kansascity-supervisors@tyson.com", subject, body);
                await emailSender.SendEmailAsync("dl-kansascity-leadership@tyson.com", subject, body);
                //List<AppUserModel> appUsers = MaintenanceLibrary.BusinessLogic.AppUserProcessor
                //    .GetAllCurrentEmployeesByRoleName("Admin", "Supervisor");

                //foreach (var appUser in appUsers)
                //{
                //await emailSender.SendEmailAsync(appUser.Email, subject, body);
                //}

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Sends information about the downtime issue to person doing follow up and the requester, when someone is given a follow up.
        /// Beware: This function is saved in Hangfire so that it can be run later.
        /// </summary>
        /// <param name="emailSender">Used to send the email</param>
        /// <param name="downtimeIssue"><see cref="DowntimeIssueModel"/> representing downtime issue before referred to</param>
        /// <param name="currentUser">A <see cref="AppUser"/> representing the user creating the downtime issue follow up</param>
        /// <returns>A <see cref="Task"/> that sends the email</returns>
        public static async Task SendEmailAboutDowntimeIssue(EmailSender emailSender, DowntimeIssueModel downtimeIssue, AppUser currentUser)
        {
            try
            {
                string subject;
                string body;
                AppUserModel personFollowingUpWith = downtimeIssue.DowntimeIssue_Followups[0].Employee;


                subject = "TEST Maintenance Application TEST New Follow up";
                body = "";
                body += "<H1>TEST OF MAINTENANCE WEB APPLICATION</H1>";
                body += "<A href='https://kkpwrwcip01.tyson.com/'>Link to Maintenance Website</A><BR>";
                body += "Downtime Issue Follow up for " + personFollowingUpWith.FullName + "<BR>";
                body += "<div style=\"border: 5px solid gray; border - radius: 8px; padding: 5px 5px 5px 5px; \">";
                body += "<H2 style=\"text - align: center; \">Downtime Issue</H2>";
                body += "<TABLE  border='1'>";
                body += "<TR><TD>Date Created:</TD><TD>" + downtimeIssue.Created + "</TD></TR>";
                body += "<TR><TD>Created By:</TD><TD>" + downtimeIssue.Employee.FullName + "</TD></TR>";
                body += "<TR><TD>Area:</TD><TD>" + downtimeIssue.Equipment.Area.Name + "</TD></TR>";
                body += "<TR><TD>Equipment:</TD><TD>" + downtimeIssue.Equipment.Name + "</TD></TR>";
                body += "<TR><TD>Downtime:</TD><TD>" + downtimeIssue.DownTime + "</TD></TR>";
                body += "<TR><TD>Issue Resolution:</TD><TD>" + downtimeIssue.IssueResolution + "</TD></TR>";
                body += "</TABLE>";
                body += "<BR>";

                body += "<TABLE>";
                body += "   <TR>";
                body += "       <TH>Employee Following Up With</TH>";
                body += "       <TH>Follow Up Reason</TH>";
                body += "       <TH>Comments</TH>";
                body += "   </TR>";

                foreach (var downtimeIssueFollowUp in downtimeIssue.DowntimeIssue_Followups)
                {
                    body += "   <TR>";
                    body += "       <TD>" + downtimeIssueFollowUp.Employee.FullName + "</TD>";
                    body += "       <TD>" + downtimeIssueFollowUp.FollowingUpReason + "</TD>";
                    body += "       <TD>" + downtimeIssueFollowUp.SupervisorComments + "</TD>";
                    body += "   </TR>";
                }

                body += "</TABLE>";
                body += "</DIV>";

                List<AppUserModel> appUsers = MaintenanceLibrary.BusinessLogic.AppUserProcessor
                    .GetAllCurrentEmployeesByRoleName("Admin", "Supervisor");

                await emailSender.SendEmailAsync(personFollowingUpWith.Email, subject, body);
                await emailSender.SendEmailAsync(currentUser.Email, subject, body);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Sends information abotu the Supervisor's Note to person doing the follow up and the requester,
        /// whens someone requests a follow up.
        /// </summary>
        /// <param name="emailSender">Used to send the email</param>
        /// <param name="supervisorsNote"><see cref="SupervisorsNoteModel"/> representing the Supervisor's Note
        /// being referenced</param>
        /// <param name="currentUser">A <see cref="AppUser"/> representing the user creating the follow up</param>
        /// <returns>A <see cref="Task"/> that sends the email</returns>
        public static async Task SendEmailAboutSupervisorsNote(EmailSender emailSender, SupervisorsNoteModel supervisorsNote, AppUser currentUser)
        {
            string subject;
            string body;
            AppUserModel personFollowingUpWith = supervisorsNote.SupervisorsNote_Followups[0].Employee;


            subject = "Supervisor's Note Follow up for " + personFollowingUpWith.FullName;

            body = "";
            body += "<A href='https://kkpwrwcip01.tyson.com/'>Link to Maintenance Website</A><BR>";
            body += "Supervisor's Note Follow up for " + personFollowingUpWith.FullName + "<BR>";
            body += "<div style=\"border: 5px solid gray; border - radius: 8px; padding: 5px 5px 5px 5px; \">";
            body += "<H2 style=\"text - align: center; \">Supervisor's Note</H2>";
            body += "<TABLE  border='1'>";
            body += "<TR><TD>Date Created:</TD><TD>" + supervisorsNote.DateCreated + "</TD></TR>";
            body += "<TR><TD>Creater:</TD><TD>" + supervisorsNote.Employee.FullName + "</TD></TR>";
            body += "<TR><TD>Area Name:</TD><TD>" + supervisorsNote.Area.Name + "</TD></TR>";
            body += "<TR><TD>Equipment Name:</TD><TD>" + supervisorsNote.Equipment.Name + "</TD></TR>";
            body += "<TR><TD>Issue:</TD><TD>" + supervisorsNote.Issue + "</TD></TR>";
            body += "</TABLE>";
            body += "<BR>";

            body += "<TABLE>";
            body += "   <TR>";
            body += "       <TH>Employee Following Up With</TH>";
            body += "       <TH>Comments</TH>";
            body += "   </TR>";

            foreach (var SupervisorsNote_Followup in supervisorsNote.SupervisorsNote_Followups)
            {
                body += "   <TR>";
                body += "       <TD>" + SupervisorsNote_Followup.Employee.FullName + "</TD>";
                body += "       <TD>" + SupervisorsNote_Followup.Comment + "</TD>";
                body += "   </TR>";
            }
            body += "</TABLE>";

            body += "</DIV>";

            await emailSender.SendEmailAsync(personFollowingUpWith.Email, subject, body);
            await emailSender.SendEmailAsync(currentUser.Email, subject, body);
        }


        public static async Task SendEmailAboutRepairPartsMonday(EmailSender emailSender)
        {
            try
            {
                var repairparts = MaintenanceLibrary.BusinessLogic
                    .RepairPartsProcessor.GetRepairParts_After_PromiseDate();

                string subject;
                string body;


                subject = "TEST Maintenance Application TEST Repair Parts Past Promise Date";
                body = "";
                body += "<H1>TEST OF MAINTENANCE WEB APPLICATION</H1>";
                body += "<A href='https://kkpwrwcip01.tyson.com/'>Link to Maintenance Website</A><BR>";
                body += "<H1>MRO With Overdue Promise Date</H1>";
                body += "<BR>";
                body += "<TABLE  border='1'>";
                body += "<TR>";
                body += "   <TH>RMA #</TH>";
                body += "   <TH>Description</TH>";
                body += "   <TH>Date Created</TH>";
                body += "   <TH>Promise Date</TH>";
                body += "   <TH>SAP Part #</TH>";
                body += "   <TH>Vendor</TH>";
                body += "</TR>";
                foreach (var repairpart in repairparts)
                {
                    body += "<TR>";
                    body += $"  <TD> {repairpart.RMANum}</TD>";
                    body += $"  <TD> {repairpart.Description}</TD>";
                    body += $"  <TD> {repairpart.CreatedDate}</TD>";
                    body += $"  <TD> {repairpart.PromiseDate}</TD>";
                    body += $"  <TD> {repairpart.SAPPartNum}</TD>";
                    body += $"  <TD> {repairpart.Vendor.Name}</TD>";
                    body += "</TR>";
                }
                body += "</TABLE>";

                List<AppUserModel> appUsers = MaintenanceLibrary.BusinessLogic.AppUserProcessor
                    .GetAllCurrentEmployeesByRoleName("MRO Supervisor");

                foreach (var appUser in appUsers)
                {
                    await emailSender.SendEmailAsync(appUser.Email, subject, body);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}


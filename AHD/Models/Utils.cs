using AHD.App_Start;
using AHD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AHD.Models
{
    public class Utils
    {
        public string generateLeaveCancelationUiRender(bool isOwner, bool ishcm, bool isApprover, NueRequestModel userRequest, NueUserProfile user, List<NueUserProfile> userList)
        {
            string uiRender = "";
            string uiMenuRender = "";

            NeuLeaveCancelation neuLeaveCancelationReq = (NeuLeaveCancelation)userRequest.requestPayload;
            var app = neuLeaveCancelationReq.isApprovalProcess;
            ApprovalProcess approvalProcess = neuLeaveCancelationReq.approvalProcess;
            var approvals = approvalProcess.requestApprovals;
            string approverStr = "";
            if (app && approvals != null && approvals.Count > 0)
            {
                for (int i = 0; i < approvals.Count; i++)
                {
                    RequestApproval requestApproval = approvals.ElementAt(i);
                    var stage = requestApproval.requestStatusStage;
                    var userApp = userList.Where(x => x.ntplId == requestApproval.ntplId).First<NueUserProfile>();
                    approverStr += "<strong>Approver("+ stage + ") :</strong> "+ userApp.fullName + "<br/>";
                }
            }

            if (userRequest.requestStatus == RequestStatus.close)
            {

            }
            else if (userRequest.requestStatus == RequestStatus.withdraw)
            {
                
            }
            else if (userRequest.requestStatus == RequestStatus.completed)
            {
                if (isOwner)
                {
                    uiMenuRender += "<button type=\"button\" class=\"btn btn-sm btn-outline-secondary\" data-toggle=\"modal\" data-target=\"#commentModal-1\"><i class=\"mdi mdi-comment-outline text-primary\"></i> Comment </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-outline-secondary\" data-toggle=\"modal\" data-target=\"#fileAttchmentModal-1\"><i class=\"mdi mdi-attachment text-primary\"></i> Attach File </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-outline-secondary\" onclick=\"showSwal('close-leave-cancelation-request')\"><i class=\"mdi mdi-close-circle-outline text-primary\"></i> Close </button>\r\n";
                }
                else if(isApprover || ishcm)
                {
                    uiMenuRender += "<button type=\"button\" class=\"btn btn-sm btn-outline-secondary\" data-toggle=\"modal\" data-target=\"#commentModal-1\"><i class=\"mdi mdi-comment-outline text-primary\"></i> Comment </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-outline-secondary\" data-toggle=\"modal\" data-target=\"#fileAttchmentModal-1\"><i class=\"mdi mdi-attachment text-primary\"></i> Attach File </button>\r\n";
                }
            }
            else
            {
                if (isOwner)
                {
                    uiMenuRender += "<button type=\"button\" class=\"btn btn-sm btn-outline-secondary\" data-toggle=\"modal\" data-target=\"#commentModal-1\"><i class=\"mdi mdi-comment-outline text-primary\"></i> Comment </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-outline-secondary\" data-toggle=\"modal\" data-target=\"#fileAttchmentModal-1\"><i class=\"mdi mdi-attachment text-primary\"></i> Attach File </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-outline-secondary\" onclick=\"showSwal('withdraw-leave-cancelation-request')\"><i class=\"mdi mdi-compare text-primary\"></i> Withdraw </button>\r\n";
                   
                }
                if (isApprover)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-outline-secondary\" onclick=\"showSwal('inter-approve-leave-cancelation-request')\"><i class=\"mdi mdi-compare text-primary\"></i> Approve </button>\r\n";
                }
                if (ishcm)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-outline-secondary\" onclick=\"showSwal('final-approve-leave-cancelation-request')\"><i class=\"mdi mdi-compare text-primary\"></i> HCM Approve </button>";
                }
            }

            uiRender += "<div class=\"row\">\r\n" +
            "            <div class=\"col-md-12 mb-5 mt-4\">\r\n" +
            "                <div class=\"btn-toolbar\">\r\n" +
            "                    <div class=\"btn-group\">\r\n" +
            uiMenuRender +
            "                    </div>\r\n" +
            "                </div>\r\n" +
            "            </div>\r\n" +
            "        </div>\r\n" +
            "        <ul class=\"nav nav-tabs tab-basic\" role=\"tablist\">\r\n" +
            "            <li class=\"nav-item\">\r\n" +
            "                <a class=\"nav-link active\" id=\"home-tab\" data-toggle=\"tab\" href=\"#whoweare\" role=\"tab\" aria-controls=\"whoweare\" aria-selected=\"true\">Req#: &nbsp; <b class=\"req-number\">" + userRequest.requestId + "</b></a>\r\n" +
            "            </li>\r\n" +
            "        </ul>\r\n" +
            "        <div class=\"tab-content tab-body tab-content-basic\">\r\n" +
            "            <div class=\"tab-pane fade pr-3 active show\">\r\n" +
            "\r\n" +
            "                <table class=\"table table-borderless w-100 mt-4\">\r\n" +
            "                    <tbody>\r\n" +
            "                        <tr>\r\n" +
            "                            <td><strong>Request Type :</strong> Leave Cancelation</td>\r\n" +
            "                            <td><strong>Creation Date :</strong> " + userRequest.dateCreated.ToLocalTime() + "</td>\r\n" +
            "                        </tr>\r\n" +
            "                        <tr>\r\n" +
            "                            <td>" + approverStr + "</td>\r\n" +
            "                            <td> <strong>Status :</strong> " + userRequest.requestStatus + " </td>\r\n" +
            "                        </tr>\r\n" +
            "                        <tr>\r\n" +
            "                            <td><strong>Leave Start Date :</strong> " + neuLeaveCancelationReq.leaveStartDate + ".</td>\r\n" +
            "                            <td><strong>Leave End Date :</strong> " + neuLeaveCancelationReq.leaveEndDate + "</td>\r\n" +
            "                        </tr>\r\n" +
            "                        <tr>\r\n" +
            "                            <td colspan=\"2\"><strong>User Request :</strong> " + neuLeaveCancelationReq.message + "</td>\r\n" +
            "                        </tr>\r\n" +
            "                    </tbody>\r\n" +
            "                </table>\r\n" +
            "\r\n" +
            "            </div>\r\n" +
            "            <div class=\"tab-pane fade active show\">\r\n" +
            "                <div class=\"horizontal-timeline\">\r\n" +
            "                    <section class=\"time-frame\">\r\n" +
            "                        <h5 class=\"section-time-frame\">Rquest Logs</h5>\r\n";

            foreach (RequestLog requestLog in userRequest.requestLogs)
            {
                var userApp = userList.Where(x => x.ntplId == requestLog.ntplId).First<NueUserProfile>();
                uiRender += "                        <div class=\"event\">\r\n" +
                "                            <p class=\"event-text\">"+ userApp.fullName + "</p>\r\n" +
                "                            <div class=\"event-alert\">"+ cleanLogMessage(requestLog.userComment) + "</div>\r\n" +
                "                            <div class=\"event-info\">"+ requestLog.dateCreated.ToLocalTime() + "</div>\r\n" +
                "                        </div>\r\n";
            }


            /*"                        <div class=\"event\">\r\n" +
            "                            <p class=\"event-text\">Monin Jose</p>\r\n" +
            "                            <div class=\"event-alert\">You have added task #26 Successfully to the project “Agile CRM”</div>\r\n" +
            "                            <div class=\"event-info\">Added comments - 5/11/2019 5:12:14 PM</div>\r\n" +
            "                        </div>\r\n" +
            "                        <div class=\"event\">\r\n" +
            "                            <p class=\"event-text\">Mathew Job</p>\r\n" +
            "                            <div class=\"event-alert\">Approved</div>\r\n" +
            "                            <div class=\"event-info\">Added comments - 5/11/2019 5:12:14 PM</div>\r\n" +
            "                        </div>\r\n" +
            "                        <div class=\"event\">\r\n" +
            "                            <p class=\"event-text\">Request Approved by approver 1</p>\r\n" +
            "                            <div class=\"event-alert\"><div class=\"badge badge-outline-success\">Approved</div></div>\r\n" +
            "                            <div class=\"event-info\">5/11/2019 5:12:14 PM</div>\r\n" +
            "                        </div>\r\n" +
            "                        <div class=\"event\">\r\n" +
            "                            <p class=\"event-text\">Request Approved by approver 1</p>\r\n" +
            "                            <div class=\"event-alert\"><div class=\"badge badge-outline-success\">Approved</div></div>\r\n" +
            "                            <div class=\"event-info\">5/11/2019 5:12:14 PM</div>\r\n" +
            "                        </div>\r\n" +
            "                        <div class=\"event\">\r\n" +
            "                            <p class=\"event-text\">Request Approved by approver 1</p>\r\n" +
            "                            <div class=\"event-alert\"><div class=\"badge badge-outline-success\">Approved</div></div>\r\n" +
            "                            <div class=\"event-info\">5/11/2019 5:12:14 PM</div>\r\n" +
            "                        </div>\r\n";*/

            uiRender += "                    </section>\r\n" +
            "                </div>\r\n" +
            "            </div>\r\n" +
            "            <div class=\"row\">\r\n" +
            "                <div class=\"col-12\">\r\n" +
            "                    <h5 class=\"section-time-frame\">Attachments</h5>\r\n" +
            "                    <div class=\"email-wrapper\">\r\n" +
            "                        <div class=\"message-body\">\r\n" +
            "                            <div class=\"attachments-sections\">\r\n" +
            "                                <ul>\r\n";
            foreach (AttachmentLog attachmentLog in userRequest.attachmentLogs)
            {
                uiRender += "                                    <li>\r\n" +
                "                                        <div class=\"thumb\"><i class=\"mdi mdi-attachment\"></i></div>\r\n" +
                "                                        <div class=\"details\">\r\n" +
                "                                            <p class=\"file-name\">"+ attachmentLog.fileName+ attachmentLog.fileExt + "</p>\r\n" +
                "                                            <div class=\"buttons\">\r\n" +
                "                                                <a href=\"/HcmAHDDashboard/DownloadAttachment?requestId="+ userRequest.requestId + "&vFile="+ attachmentLog.VFileName + "\" target=\"_blank\" class=\"download\">Download</a>\r\n" +
                "                                            </div>\r\n" +
                "                                        </div>\r\n" +
                "                                    </li>\r\n";
            }

            /*"                                    <li>\r\n" +
            "                                        <div class=\"thumb\"><i class=\"mdi mdi-attachment\"></i></div>\r\n" +
            "                                        <div class=\"details\">\r\n" +
            "                                            <p class=\"file-name\">Seminar Reports.pdf</p>\r\n" +
            "                                            <div class=\"buttons\">\r\n" +
            "                                                <a href=\"#\" class=\"download\">Download</a>\r\n" +
            "                                            </div>\r\n" +
            "                                        </div>\r\n" +
            "                                    </li>\r\n" +
            "                                    <li>\r\n" +
            "                                        <div class=\"thumb\"><i class=\"mdi mdi-attachment\"></i></div>\r\n" +
            "                                        <div class=\"details\">\r\n" +
            "                                            <p class=\"file-name\">Product Design.jpg</p>\r\n" +
            "                                            <div class=\"buttons\">\r\n" +
            "                                                <a href=\"#\" class=\"download\">Download</a>\r\n" +
            "                                            </div>\r\n" +
            "                                        </div>\r\n" +
            "                                    </li>\r\n";*/
            uiRender += "                                </ul>\r\n" +
            "                            </div>\r\n" +
            "                        </div>\r\n" +
            "                    </div>\r\n" +
            "                </div>\r\n" +
            "            </div>\r\n" +
            "        </div>";


            return uiRender;
        }

        public string generatePastLeaveUiRender(bool isOwner, bool ishcm, bool isApprover, NueRequestModel userRequest, NueUserProfile user, List<NueUserProfile> userList)
        {
            string uiRender = "";
            string uiMenuRender = "";

            NeuLeavePastApply neuLeavePastApplyReq = (NeuLeavePastApply)userRequest.requestPayload;
            var app = neuLeavePastApplyReq.isApprovalProcess;
            ApprovalProcess approvalProcess = neuLeavePastApplyReq.approvalProcess;
            var approvals = approvalProcess.requestApprovals;
            string approverStr = "";
            if (app && approvals != null && approvals.Count > 0)
            {
                for (int i = 0; i < approvals.Count; i++)
                {
                    RequestApproval requestApproval = approvals.ElementAt(i);
                    var stage = requestApproval.requestStatusStage;
                    var userApp = userList.Where(x => x.ntplId == requestApproval.ntplId).First<NueUserProfile>();
                    approverStr += "<strong>Approver(" + stage + ") :</strong> " + userApp.fullName + "<br/>";
                }
            }

            if (userRequest.requestStatus == RequestStatus.close)
            {

            }
            else if (userRequest.requestStatus == RequestStatus.withdraw)
            {

            }
            else if (userRequest.requestStatus == RequestStatus.completed)
            {
                if (isOwner)
                {
                    uiMenuRender += "<button type=\"button\" class=\"btn btn-sm btn-outline-secondary\" data-toggle=\"modal\" data-target=\"#commentModal-1\"><i class=\"mdi mdi-comment-outline text-primary\"></i> Comment </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-outline-secondary\" data-toggle=\"modal\" data-target=\"#fileAttchmentModal-1\"><i class=\"mdi mdi-attachment text-primary\"></i> Attach File </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-outline-secondary\" onclick=\"showSwal('close-leave-past-apply-request')\"><i class=\"mdi mdi-close-circle-outline text-primary\"></i> Close </button>\r\n";
                }
                else if (isApprover || ishcm)
                {
                    uiMenuRender += "<button type=\"button\" class=\"btn btn-sm btn-outline-secondary\" data-toggle=\"modal\" data-target=\"#commentModal-1\"><i class=\"mdi mdi-comment-outline text-primary\"></i> Comment </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-outline-secondary\" data-toggle=\"modal\" data-target=\"#fileAttchmentModal-1\"><i class=\"mdi mdi-attachment text-primary\"></i> Attach File </button>\r\n";
                }
            }
            else
            {
                if (isOwner)
                {
                    uiMenuRender += "<button type=\"button\" class=\"btn btn-sm btn-outline-secondary\" data-toggle=\"modal\" data-target=\"#commentModal-1\"><i class=\"mdi mdi-comment-outline text-primary\"></i> Comment </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-outline-secondary\" data-toggle=\"modal\" data-target=\"#fileAttchmentModal-1\"><i class=\"mdi mdi-attachment text-primary\"></i> Attach File </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-outline-secondary\" onclick=\"showSwal('withdraw-leave-past-apply-request')\"><i class=\"mdi mdi-compare text-primary\"></i> Withdraw </button>\r\n";

                }
                if (isApprover)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-outline-secondary\" onclick=\"showSwal('inter-approve-leave-past-apply-request')\"><i class=\"mdi mdi-compare text-primary\"></i> Approve </button>\r\n";
                }
                if (ishcm)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-outline-secondary\" onclick=\"showSwal('final-approve-leave-past-apply-request')\"><i class=\"mdi mdi-compare text-primary\"></i> HCM Approve </button>";
                }
            }

            uiRender += "<div class=\"row\">\r\n" +
            "            <div class=\"col-md-12 mb-5 mt-4\">\r\n" +
            "                <div class=\"btn-toolbar\">\r\n" +
            "                    <div class=\"btn-group\">\r\n" +
            uiMenuRender +
            "                    </div>\r\n" +
            "                </div>\r\n" +
            "            </div>\r\n" +
            "        </div>\r\n" +
            "        <ul class=\"nav nav-tabs tab-basic\" role=\"tablist\">\r\n" +
            "            <li class=\"nav-item\">\r\n" +
            "                <a class=\"nav-link active\" id=\"home-tab\" data-toggle=\"tab\" href=\"#whoweare\" role=\"tab\" aria-controls=\"whoweare\" aria-selected=\"true\">Req#: &nbsp; <b class=\"req-number\">" + userRequest.requestId + "</b></a>\r\n" +
            "            </li>\r\n" +
            "        </ul>\r\n" +
            "        <div class=\"tab-content tab-body tab-content-basic\">\r\n" +
            "            <div class=\"tab-pane fade pr-3 active show\">\r\n" +
            "\r\n" +
            "                <table class=\"table table-borderless w-100 mt-4\">\r\n" +
            "                    <tbody>\r\n" +
            "                        <tr>\r\n" +
            "                            <td><strong>Request Type :</strong> Past Leave Apply</td>\r\n" +
            "                            <td><strong>Creation Date :</strong> " + userRequest.dateCreated.ToLocalTime() + "</td>\r\n" +
            "                        </tr>\r\n" +
            "                        <tr>\r\n" +
            "                            <td>" + approverStr + "</td>\r\n" +
            "                            <td> <strong>Status :</strong> " + userRequest.requestStatus + " </td>\r\n" +
            "                        </tr>\r\n" +
            "                        <tr>\r\n" +
            "                            <td><strong>Leave Start Date :</strong> " + neuLeavePastApplyReq.leaveStartDate + ".</td>\r\n" +
            "                            <td><strong>Leave End Date :</strong> " + neuLeavePastApplyReq.leaveEndDate + "</td>\r\n" +
            "                        </tr>\r\n" +
            "                        <tr>\r\n" +
            "                            <td colspan=\"2\"><strong>User Request :</strong> " + neuLeavePastApplyReq.message + "</td>\r\n" +
            "                        </tr>\r\n" +
            "                    </tbody>\r\n" +
            "                </table>\r\n" +
            "\r\n" +
            "            </div>\r\n" +
            "            <div class=\"tab-pane fade active show\">\r\n" +
            "                <div class=\"horizontal-timeline\">\r\n" +
            "                    <section class=\"time-frame\">\r\n" +
            "                        <h5 class=\"section-time-frame\">Rquest Logs</h5>\r\n";

            foreach (RequestLog requestLog in userRequest.requestLogs)
            {
                var userApp = userList.Where(x => x.ntplId == requestLog.ntplId).First<NueUserProfile>();
                uiRender += "                        <div class=\"event\">\r\n" +
                "                            <p class=\"event-text\">" + userApp.fullName + "</p>\r\n" +
                "                            <div class=\"event-alert\">" + cleanLogMessage(requestLog.userComment) + "</div>\r\n" +
                "                            <div class=\"event-info\">" + requestLog.dateCreated.ToLocalTime() + "</div>\r\n" +
                "                        </div>\r\n";
            }
            uiRender += "                    </section>\r\n" +
            "                </div>\r\n" +
            "            </div>\r\n" +
            "            <div class=\"row\">\r\n" +
            "                <div class=\"col-12\">\r\n" +
            "                    <h5 class=\"section-time-frame\">Attachments</h5>\r\n" +
            "                    <div class=\"email-wrapper\">\r\n" +
            "                        <div class=\"message-body\">\r\n" +
            "                            <div class=\"attachments-sections\">\r\n" +
            "                                <ul>\r\n";
            foreach (AttachmentLog attachmentLog in userRequest.attachmentLogs)
            {
                uiRender += "                                    <li>\r\n" +
                "                                        <div class=\"thumb\"><i class=\"mdi mdi-attachment\"></i></div>\r\n" +
                "                                        <div class=\"details\">\r\n" +
                "                                            <p class=\"file-name\">" + attachmentLog.fileName + attachmentLog.fileExt + "</p>\r\n" +
                "                                            <div class=\"buttons\">\r\n" +
                "                                                <a href=\"/HcmAHDDashboard/DownloadAttachment?requestId=" + userRequest.requestId + "&vFile=" + attachmentLog.VFileName + "\" target=\"_blank\" class=\"download\">Download</a>\r\n" +
                "                                            </div>\r\n" +
                "                                        </div>\r\n" +
                "                                    </li>\r\n";
            }
            uiRender += "                                </ul>\r\n" +
            "                            </div>\r\n" +
            "                        </div>\r\n" +
            "                    </div>\r\n" +
            "                </div>\r\n" +
            "            </div>\r\n" +
            "        </div>";


            return uiRender;
        }

        public string generateWFHUiRender(bool isOwner, bool ishcm, bool isApprover, NueRequestModel userRequest, NueUserProfile user, List<NueUserProfile> userList)
        {
            string uiRender = "";
            string uiMenuRender = "";

            NeuLeaveWFHApply neuLeaveWFHApplyReq = (NeuLeaveWFHApply)userRequest.requestPayload;
            ApprovalProcess approvalProcess = neuLeaveWFHApplyReq.approvalProcess;
            
            if (userRequest.requestStatus == RequestStatus.close)
            {

            }
            else if (userRequest.requestStatus == RequestStatus.withdraw)
            {

            }
            else if (userRequest.requestStatus == RequestStatus.completed)
            {
                if (isOwner)
                {
                    uiMenuRender += "<button type=\"button\" class=\"btn btn-sm btn-outline-secondary\" data-toggle=\"modal\" data-target=\"#commentModal-1\"><i class=\"mdi mdi-comment-outline text-primary\"></i> Comment </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-outline-secondary\" data-toggle=\"modal\" data-target=\"#fileAttchmentModal-1\"><i class=\"mdi mdi-attachment text-primary\"></i> Attach File </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-outline-secondary\" onclick=\"showSwal('close-leave-wfh-apply-request')\"><i class=\"mdi mdi-close-circle-outline text-primary\"></i> Close </button>\r\n";
                }
                else if (ishcm)
                {
                    uiMenuRender += "<button type=\"button\" class=\"btn btn-sm btn-outline-secondary\" data-toggle=\"modal\" data-target=\"#commentModal-1\"><i class=\"mdi mdi-comment-outline text-primary\"></i> Comment </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-outline-secondary\" data-toggle=\"modal\" data-target=\"#fileAttchmentModal-1\"><i class=\"mdi mdi-attachment text-primary\"></i> Attach File </button>\r\n";
                }
            }
            else
            {
                if (isOwner)
                {
                    uiMenuRender += "<button type=\"button\" class=\"btn btn-sm btn-outline-secondary\" data-toggle=\"modal\" data-target=\"#commentModal-1\"><i class=\"mdi mdi-comment-outline text-primary\"></i> Comment </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-outline-secondary\" data-toggle=\"modal\" data-target=\"#fileAttchmentModal-1\"><i class=\"mdi mdi-attachment text-primary\"></i> Attach File </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-outline-secondary\" onclick=\"showSwal('withdraw-leave-wfh-apply-request')\"><i class=\"mdi mdi-compare text-primary\"></i> Withdraw </button>\r\n";
                }
                if (ishcm)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-outline-secondary\" onclick=\"showSwal('final-approve-leave-wfh-apply-request')\"><i class=\"mdi mdi-compare text-primary\"></i> HCM Approve </button>";
                }
            }
            
            uiRender += "<div class=\"row\">\r\n" +
            "            <div class=\"col-md-12 mb-5 mt-4\">\r\n" +
            "                <div class=\"btn-toolbar\">\r\n" +
            "                    <div class=\"btn-group\">\r\n" +
            uiMenuRender +
            "                    </div>\r\n" +
            "                </div>\r\n" +
            "            </div>\r\n" +
            "        </div>\r\n" +
            "        <ul class=\"nav nav-tabs tab-basic\" role=\"tablist\">\r\n" +
            "            <li class=\"nav-item\">\r\n" +
            "                <a class=\"nav-link active\" id=\"home-tab\" data-toggle=\"tab\" href=\"#whoweare\" role=\"tab\" aria-controls=\"whoweare\" aria-selected=\"true\">Req#: &nbsp; <b class=\"req-number\">" + userRequest.requestId + "</b></a>\r\n" +
            "            </li>\r\n" +
            "        </ul>\r\n" +
            "        <div class=\"tab-content tab-body tab-content-basic\">\r\n" +
            "            <div class=\"tab-pane fade pr-3 active show\">\r\n" +
            "\r\n" +
            "                <table class=\"table table-borderless w-100 mt-4\">\r\n" +
            "                    <tbody>\r\n" +
            "                        <tr>\r\n" +
            "                            <td><strong>Request Type :</strong> Work From Home</td>\r\n" +
            "                            <td><strong>Creation Date :</strong> " + userRequest.dateCreated.ToLocalTime() + "</td>\r\n" +
            "                        </tr>\r\n" +
            "                        <tr>\r\n" +
            "                            <td> <strong>Status :</strong> " + userRequest.requestStatus + " </td>\r\n" +
            "                        </tr>\r\n" +
            "                        <tr>\r\n" +
            "                            <td><strong>Leave Start Date :</strong> " + neuLeaveWFHApplyReq.leaveStartDate + ".</td>\r\n" +
            "                            <td><strong>Leave End Date :</strong> " + neuLeaveWFHApplyReq.leaveEndDate + "</td>\r\n" +
            "                        </tr>\r\n" +
            "                        <tr>\r\n" +
            "                            <td colspan=\"2\"><strong>User Request :</strong> " + neuLeaveWFHApplyReq.message + "</td>\r\n" +
            "                        </tr>\r\n" +
            "                    </tbody>\r\n" +
            "                </table>\r\n" +
            "\r\n" +
            "            </div>\r\n" +
            "            <div class=\"tab-pane fade active show\">\r\n" +
            "                <div class=\"horizontal-timeline\">\r\n" +
            "                    <section class=\"time-frame\">\r\n" +
            "                        <h5 class=\"section-time-frame\">Rquest Logs</h5>\r\n";

            foreach (RequestLog requestLog in userRequest.requestLogs)
            {
                var userApp = userList.Where(x => x.ntplId == requestLog.ntplId).First<NueUserProfile>();
                uiRender += "                        <div class=\"event\">\r\n" +
                "                            <p class=\"event-text\">" + userApp.fullName + "</p>\r\n" +
                "                            <div class=\"event-alert\">" + cleanLogMessage(requestLog.userComment) + "</div>\r\n" +
                "                            <div class=\"event-info\">" + requestLog.dateCreated.ToLocalTime() + "</div>\r\n" +
                "                        </div>\r\n";
            }
            uiRender += "                    </section>\r\n" +
            "                </div>\r\n" +
            "            </div>\r\n" +
            "            <div class=\"row\">\r\n" +
            "                <div class=\"col-12\">\r\n" +
            "                    <h5 class=\"section-time-frame\">Attachments</h5>\r\n" +
            "                    <div class=\"email-wrapper\">\r\n" +
            "                        <div class=\"message-body\">\r\n" +
            "                            <div class=\"attachments-sections\">\r\n" +
            "                                <ul>\r\n";
            foreach (AttachmentLog attachmentLog in userRequest.attachmentLogs)
            {
                uiRender += "                                    <li>\r\n" +
                "                                        <div class=\"thumb\"><i class=\"mdi mdi-attachment\"></i></div>\r\n" +
                "                                        <div class=\"details\">\r\n" +
                "                                            <p class=\"file-name\">" + attachmentLog.fileName + attachmentLog.fileExt + "</p>\r\n" +
                "                                            <div class=\"buttons\">\r\n" +
                "                                                <a href=\"/HcmAHDDashboard/DownloadAttachment?requestId=" + userRequest.requestId + "&vFile=" + attachmentLog.VFileName + "\" target=\"_blank\" class=\"download\">Download</a>\r\n" +
                "                                            </div>\r\n" +
                "                                        </div>\r\n" +
                "                                    </li>\r\n";
            }
            uiRender += "                                </ul>\r\n" +
            "                            </div>\r\n" +
            "                        </div>\r\n" +
            "                    </div>\r\n" +
            "                </div>\r\n" +
            "            </div>\r\n" +
            "        </div>";


            return uiRender;
        }

        public string cleanLogMessage(string message)
        {
            string messageReturn = "";
            if (message.Contains("neulog>"))
            {
                int pFrom = message.IndexOf("<neulog>") + "<neulog>".Length;
                int pTo = message.LastIndexOf("</neulog>");
                string result = message.Substring(pFrom, pTo - pFrom);
                messageReturn = "<div class=\"badge badge-outline-success\">"+ result + "</div>";
            }
            else
            {
                messageReturn = message;
            }
            return messageReturn;
        }
    }
}
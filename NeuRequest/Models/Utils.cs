using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeuRequest.Models
{
    public class Utils
    {
        public string generateLeaveCancelationUiRender(bool isOwner, bool ishcm, bool isApprover, UserProfile currentUser, UserRequest userRequest, NeuLeaveCancelationModal neuLeaveCancelationModal, List<NueRequestAceessLog> nueRequestAceessLogs, List<UserProfile> userProfiles, List<NueRequestActivityModel> nueRequestActivityModels, List<AttachmentLogModel> attachmentLogModels)
        {
            string uiRender = "";
            string uiMenuRender = "";
            string approverStr = "";

            UserProfile requestOwner = userProfiles.Where(x => x.Id == userRequest.OwnerId).First<UserProfile>();

            foreach (NueRequestAceessLog nueRequestAceessLog in nueRequestAceessLogs)
            {
                if(nueRequestAceessLog.UserId != nueRequestAceessLog.OwnerId)
                {
                    var userApp = userProfiles.Where(x => x.Id == nueRequestAceessLog.UserId).First<UserProfile>();
                    approverStr += "                            <h5 class=\"p-t-20\">Ticket Approver</h5>\r\n" +
                    "                            <span>" + userApp.FullName + " (" + userApp.NTPLID + ")</span>\r\n" +
                    "                            <br>\r\n";
                }
            }

            if (userRequest.RequestStatus == "close")
            {

            }
            else if (userRequest.RequestStatus == "withdraw")
            {

            }
            else if (userRequest.RequestStatus == "completed")
            {
                if (isOwner)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#commentModal-1\"><i class=\"mdi mdi-comment-outline\"></i> Comment </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#fileAttchmentModal-1\"><i class=\"mdi mdi-attachment\"></i> Attach File </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" onclick=\"showSwal('close-leave-cancelation-request')\"><i class=\"mdi mdi-close-circle-outline\"></i> Close </button>\r\n";
                }
                else if (isApprover || ishcm)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#commentModal-1\"><i class=\"mdi mdi-comment-outline\"></i> Comment </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#fileAttchmentModal-1\"><i class=\"mdi mdi-attachment\"></i> Attach File </button>\r\n";
                }
            }
            else
            {
                uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#commentModal-1\"><i class=\"mdi mdi-comment-outline\"></i> Comment </button>\r\n";
                uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#fileAttchmentModal-1\"><i class=\"mdi mdi-attachment\"></i> Attach File </button>\r\n";
                if (isOwner)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" onclick=\"showSwal('withdraw-leave-cancelation-request')\"><i class=\"mdi mdi-compare text-primary\"></i> Withdraw </button>\r\n";
                }
                if (isApprover)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" onclick=\"showSwal('inter-approve-leave-cancelation-request')\"><i class=\"mdi mdi-compare text-primary\"></i> Approve </button>\r\n";
                }
                if (ishcm)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" onclick=\"showSwal('final-approve-leave-cancelation-request')\"><i class=\"mdi mdi-compare text-primary\"></i> Approve Request </button>";
                }
            }

            string requestStatusStr = "";
            if (userRequest.RequestStatus == "close")
            {
                requestStatusStr = "                                    <span class=\"label label-dark\">Close</span>\r\n";
            }
            else if (userRequest.RequestStatus == "completed")
            {
                requestStatusStr = "                                    <span class=\"label label-success\">Completed</span>\r\n";
            }
            else if (userRequest.RequestStatus == "withdraw")
            {
                requestStatusStr = "                                    <span class=\"label label-danger\">Withdraw</span>\r\n";
            }
            else if (userRequest.RequestStatus == "In_Approval")
            {
                requestStatusStr = "                                    <span class=\"label label-warning\">In Approval</span>\r\n";
            }
            else if (userRequest.RequestStatus == "created")
            {
                requestStatusStr = "                                    <span class=\"label label-primary\">Created</span>\r\n";
            }

            uiRender += "<div class=\"row\">\r\n" +
                    "            <div class=\"col-md-12 mb-4 mt-4\">\r\n" +
                    "                <div class=\"btn-toolbar\">\r\n" +
                    "                    <div class=\"btn-group inline\">\r\n" +
                    uiMenuRender +
                    "                    </div>\r\n" +
                    "                </div>\r\n" +
                    "            </div>\r\n" +
                    "        </div>\r\n" +
                    "\r\n" +
                    "        <div class=\"ahd-service-container\">\r\n" +
                    "\r\n" +
                    "            <div class=\"row\">\r\n" +
                    "                <div class=\"col-12\">\r\n" +
                    "                    <div class=\"card\">\r\n" +
                    "                        <div class=\"card-body\">\r\n" +
                    "                            <h4 class=\"card-title\">Request: <span class=\"editable editable-click cursor-default\">#"+ userRequest.RequestId + "</span></h4>\r\n" +
                    "\r\n" +
                    "                            <div class=\"row\">\r\n" +
                    "                                <div class=\"col-8\">\r\n" +
                    "                                    <p class=\"card-description hide\">Request timeline</p>\r\n" +
                    "                                    <div class=\"mt-4\">\r\n" +
                    "                                        <div class=\"vertical-timeline\">\r\n";

            uiRender += "                                            <div class=\"timeline-wrapper timeline-wrapper-primary\">\r\n" +
                                    "                                                <div class=\"timeline-badge\"></div>\r\n" +
                                    "                                                <div class=\"timeline-panel\">\r\n" +
                                    "                                                    <div class=\"timeline-heading\">\r\n" +
                                    "                                                        <h6 class=\"timeline-title\">Request Created</h6>\r\n" +
                                    "                                                    </div>\r\n" +
                                    "                                                    <div class=\"timeline-body\">\r\n" +
                                    "                                                        <p>"+ requestOwner.FullName + " (" + requestOwner.NTPLID + ") "+((neuLeaveCancelationModal.Message != null && neuLeaveCancelationModal.Message.Trim() != "") ? neuLeaveCancelationModal.Message.Trim() : "has created new Leave Cancelation Request") +"</p>\r\n" +
                                    "                                                    </div>\r\n" +
                                    "                                                    <div class=\"timeline-footer d-flex align-items-center\">\r\n" +
                                    "                                                        <i class=\"mdi mdi-heart-outline text-muted mr-1 hide\"></i>\r\n" +
                                    "                                                        <span class=\"hide\">19</span>\r\n" +
                                    "                                                        <span class=\"ml-auto font-weight-bold\">"+ neuLeaveCancelationModal.AddedOn.ToLocalTime() + "</span>\r\n" +
                                    "                                                    </div>\r\n" +
                                    "                                                </div>\r\n" +
                                    "                                            </div>\r\n";
            uiRender += generateRequestLog(userProfiles, nueRequestActivityModels, attachmentLogModels);
           var temp ="                                            <div class=\"timeline-wrapper timeline-wrapper-warning hide\">\r\n" +
            "                                                <div class=\"timeline-badge\"></div>\r\n" +
            "                                                <div class=\"timeline-panel\">\r\n" +
            "                                                    <div class=\"timeline-heading\">\r\n" +
            "                                                        <h6 class=\"timeline-title\">Request Created</h6>\r\n" +
            "                                                    </div>\r\n" +
            "                                                    <div class=\"timeline-body\">\r\n" +
            "                                                        <p>Monin Jose (0790) has created new Leave Cancelation Request</p>\r\n" +
            "                                                    </div>\r\n" +
            "                                                    <div class=\"timeline-footer d-flex align-items-center\">\r\n" +
            "                                                        <i class=\"mdi mdi-heart-outline text-muted mr-1 hide\"></i>\r\n" +
            "                                                        <span class=\"hide\">19</span>\r\n" +
            "                                                        <span class=\"ml-auto font-weight-bold\">2019-05-31 16:40:07.033</span>\r\n" +
            "                                                    </div>\r\n" +
            "                                                </div>\r\n" +
            "                                            </div>\r\n" +

            "                                            <div class=\"timeline-wrapper timeline-inverted timeline-wrapper-warning hide\">\r\n" +
            "                                                <div class=\"timeline-badge\"></div>\r\n" +
            "                                                <div class=\"timeline-panel\">\r\n" +
            "                                                    <div class=\"timeline-heading\">\r\n" +
            "                                                        <h6 class=\"timeline-title\">Comment Added</h6>\r\n" +
            "                                                    </div>\r\n" +
            "                                                    <div class=\"timeline-body\">\r\n" +
            "                                                        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis pharetra varius quam sit amet vulputate. Quisque mauris augue,</p>\r\n" +
            "                                                    </div>\r\n" +
            "                                                    <div class=\"timeline-footer d-flex align-items-center\">\r\n" +
            "                                                        <i class=\"mdi mdi-heart-outline text-muted mr-1 hide\"></i>\r\n" +
            "                                                        <span class=\"hide\">25</span>\r\n" +
            "                                                        <span class=\"ml-auto font-weight-bold\">2019-05-31 16:40:08.033</span>\r\n" +
            "                                                    </div>\r\n" +
            "                                                </div>\r\n" +
            "                                            </div>\r\n" +

            "                                            <div class=\"timeline-wrapper timeline-wrapper-success hide\">\r\n" +
            "                                                <div class=\"timeline-badge\"></div>\r\n" +
            "                                                <div class=\"timeline-panel\">\r\n" +
            "                                                    <div class=\"timeline-heading\">\r\n" +
            "                                                        <h6 class=\"timeline-title\">New File Attached</h6>\r\n" +
            "                                                    </div>\r\n" +
            "                                                    <div class=\"timeline-body\">\r\n" +
            "                                                        <div>\r\n" +
            "                                                            <div class=\"thumb\"><i class=\"mdi mdi-attachment\"></i></div>\r\n" +
            "                                                            <div class=\"details\">\r\n" +
            "                                                                <p class=\"file-name\">favicon.png</p>\r\n" +
            "                                                                <div class=\"buttons\">\r\n" +
            "                                                                    <a href=\"/HcmAHDDashboard/DownloadAttachment?requestId=1000000000000000001&amp;vFile=20190513025413_.png\" target=\"_blank\" class=\"download\">Download</a>\r\n" +
            "                                                                </div>\r\n" +
            "                                                            </div>\r\n" +
            "                                                        </div>\r\n" +
            "                                                    </div>\r\n" +
            "                                                    <div class=\"timeline-footer d-flex align-items-center\">\r\n" +
            "                                                        <i class=\"mdi mdi-heart-outline text-muted mr-1 hide\"></i>\r\n" +
            "                                                        <span class=\"hide\">25</span>\r\n" +
            "                                                        <span class=\"ml-auto font-weight-bold\">2019-05-31 16:40:08.033</span>\r\n" +
            "                                                    </div>\r\n" +
            "                                                </div>\r\n" +
            "                                            </div>\r\n" +

            "                                            <div class=\"timeline-wrapper timeline-inverted timeline-wrapper-info hide\">\r\n" +
            "                                                <div class=\"timeline-badge\"></div>\r\n" +
            "                                                <div class=\"timeline-panel\">\r\n" +
            "                                                    <div class=\"timeline-heading\">\r\n" +
            "                                                        <h6 class=\"timeline-title\">L1 Approval</h6>\r\n" +
            "                                                    </div>\r\n" +
            "                                                    <div class=\"timeline-body\">\r\n" +
            "                                                        <p>Mathew Job (0725) approved your request</p>\r\n" +
            "                                                    </div>\r\n" +
            "                                                    <div class=\"timeline-footer d-flex align-items-center\">\r\n" +
            "                                                        <i class=\"mdi mdi-heart-outline text-muted mr-1 hide\"></i>\r\n" +
            "                                                        <span class=\"hide\">25</span>\r\n" +
            "                                                        <span class=\"ml-auto font-weight-bold\">2019-05-31 16:40:08.033</span>\r\n" +
            "                                                    </div>\r\n" +
            "                                                </div>\r\n" +
            "                                            </div>\r\n" +

            "                                            <div class=\"timeline-wrapper timeline-wrapper-primary hide\">\r\n" +
            "                                                <div class=\"timeline-badge\"></div>\r\n" +
            "                                                <div class=\"timeline-panel\">\r\n" +
            "                                                    <div class=\"timeline-heading\">\r\n" +
            "                                                        <h6 class=\"timeline-title\">Approved by HCM</h6>\r\n" +
            "                                                    </div>\r\n" +
            "                                                    <div class=\"timeline-body\">\r\n" +
            "                                                        <p>Priya Ignatius (0580) approved your request</p>\r\n" +
            "                                                    </div>\r\n" +
            "                                                    <div class=\"timeline-footer d-flex align-items-center\">\r\n" +
            "                                                        <i class=\"mdi mdi-heart-outline text-muted mr-1 hide\"></i>\r\n" +
            "                                                        <span class=\"hide\">25</span>\r\n" +
            "                                                        <span class=\"ml-auto font-weight-bold\">25th July 2016</span>\r\n" +
            "                                                    </div>\r\n" +
            "                                                </div>\r\n" +
            "                                            </div>\r\n";



                     uiRender += "                                        </div>\r\n" +
                    "                                    </div>\r\n" +
                    "                                </div>\r\n" +
                    "\r\n" +
                    "\r\n" +
                    "                                <div class=\"col-4\">\r\n" +
                    "\r\n" +
                    "                                    <div class=\"card thin-border\">\r\n" +
                    "                                        <div class=\"card-body\">\r\n" +
                    "                                            <h4 class=\"card-title\">Leave Cancelation Request</h4>\r\n" +
                    "                                        </div>\r\n" +
                    "\r\n" +
                    "                                        <div class=\"card-body bg-light\">\r\n" +
                    "                                            <div class=\"row text-center\">\r\n" +
                    "                                                <div class=\"col-6 m-t-10 m-b-10\">\r\n" +
                   requestStatusStr +
                    "                                                </div>\r\n" +
                    "                                                <div class=\"col-6 m-t-10 m-b-10\">\r\n" +
                    neuLeaveCancelationModal.AddedOn.ToLocalTime()+
                    "                                                </div>\r\n" +
                    "                                            </div>\r\n" +
                    "                                        </div>\r\n" +
                    "\r\n" +
                    "                                        <div class=\"card-body\">\r\n" +
                    "                                            <h5 class=\"p-t-20\">Ticket Creator</h5>\r\n" +
                    "                                            <span>"+ requestOwner.FullName +" ("+ requestOwner.NTPLID+ ") </span>\r\n" +
                    "                                            <br>\r\n" +
                    approverStr +
                    "                                            <h5 class=\"m-t-30\">Leave Start Date</h5>\r\n" +
                    "                                            <span>"+ neuLeaveCancelationModal.StartDate + "</span>\r\n" +
                    "                                            <br>\r\n" +
                    "                                            <h5 class=\"m-t-30\">Leave End Date</h5>\r\n" +
                    "                                            <span>"+ neuLeaveCancelationModal.EndDate + "</span>\r\n" +
                    "                                            <br>\r\n" +
                    "                                        </div>\r\n" +
                    "\r\n" +
                    "                                    </div>\r\n" +
                    "\r\n" +
                    "                                </div>\r\n" +
                    "\r\n" +
                    "\r\n" +
                    "\r\n" +
                    "                            </div>\r\n" +
                    "\r\n" +
                    "\r\n" +
                    "                        </div>\r\n" +
                    "                    </div>\r\n" +
                    "                </div>\r\n" +
                    "            </div>\r\n" +
                    "\r\n" +
                    "        </div>";

            return uiRender;
        }
        static Random rnd = new Random();
        public string generateRequestLog(List<UserProfile> userProfiles, List<NueRequestActivityModel> nueRequestActivityModels, List<AttachmentLogModel> attachmentLogModels)
        {
            List<string> colorPallet = new List<string>() {
                "timeline-wrapper-warning",
                "timeline-wrapper-danger",
                "timeline-wrapper-success",
                "timeline-wrapper-info",
                "timeline-wrapper-primary",
            };


            string uiRender = "";
            for (int i = 0; i < nueRequestActivityModels.Count; i++)
            {
                if(nueRequestActivityModels.ElementAt(i) != null)
                {
                    NueRequestActivityModel nueRequestActivityModel = nueRequestActivityModels.ElementAt(i);
                    var className = "";
                    if ((i) % 2 == 0)
                    {
                        className = " timeline-wrapper timeline-inverted "+ colorPallet[rnd.Next(colorPallet.Count)];
                    }
                    else
                    {
                        className = " timeline-wrapper " + colorPallet[rnd.Next(colorPallet.Count)];
                    }
                    string heading = "";
                    string body = "";
                    bool able = false;
                    if(nueRequestActivityModel.PayloadTypeDesc == "Comment")
                    {
                        able = true;
                        body = "                                                        <p>" + nueRequestActivityModel.FullName + " (" + nueRequestActivityModel.NTPLID + ") " + nueRequestActivityModel.Payload + "</p>\r\n";
                        heading = "                                                        <h6 class=\"timeline-title\">Comment Added</h6>\r\n";
                    }
                    else if (nueRequestActivityModel.PayloadTypeDesc == "L1 Approval")
                    {
                        able = true;
                        body = "                                                        <p>" + nueRequestActivityModel.FullName + " (" + nueRequestActivityModel.NTPLID + ") " + nueRequestActivityModel.Payload + "</p>\r\n";
                        heading = "                                                        <h6 class=\"timeline-title\">Level 1 Approval</h6>\r\n";
                    }
                    else if (nueRequestActivityModel.PayloadTypeDesc == "HCM Approval")
                    {
                        able = true;
                        body = "                                                        <p>" + nueRequestActivityModel.FullName + " (" + nueRequestActivityModel.NTPLID + ") " + nueRequestActivityModel.Payload + "</p>\r\n";
                        heading = "                                                        <h6 class=\"timeline-title\">HCM Approval</h6>\r\n";
                    }
                    else if (nueRequestActivityModel.PayloadTypeDesc == "Close")
                    {
                        able = true;
                        body = "                                                        <p>" + nueRequestActivityModel.FullName + " (" + nueRequestActivityModel.NTPLID + ") " + nueRequestActivityModel.Payload + "</p>\r\n";
                        heading = "                                                        <h6 class=\"timeline-title\">Request Closed</h6>\r\n";
                    }
                    else if (nueRequestActivityModel.PayloadTypeDesc == "Withdraw")
                    {
                        able = true;
                        body = "                                                        <p>" + nueRequestActivityModel.FullName + " (" + nueRequestActivityModel.NTPLID + ") " + nueRequestActivityModel.Payload + "</p>\r\n";
                        heading = "                                                        <h6 class=\"timeline-title\">Request Withdrawn </h6>\r\n";
                    }
                    else if (nueRequestActivityModel.PayloadTypeDesc == "File")
                    {
                        var internalFile = attachmentLogModels.Where(x => x.Request == nueRequestActivityModel.Request && x.VFileName == nueRequestActivityModel.Payload);
                        if(internalFile != null && internalFile.Count() > 0)
                        {
                            able = true;
                            AttachmentLogModel attachmentLogModel = internalFile.First();
                            UserProfile attachmentOwner = userProfiles.Where(x => x.Id == attachmentLogModel.UserId).First<UserProfile>();
                            heading = "                                                        <h6 class=\"timeline-title\"> File Attached <i class=\"mdi mdi-attachment\"></i> </h6>\r\n";
                            body = "                                                        <div>\r\n" +
                                            "                                                            <div class=\"thumb hide\"><i class=\"mdi mdi-attachment\"></i></div>\r\n" +
                                            "                                                            <div class=\"details\">\r\n" +
                                            "                                                                <p class=\"file-name hide\">"+ attachmentLogModel.FileName+""+ attachmentLogModel.FileExt + "</p>\r\n" +
                                            "                                                                <div class=\"buttons\">\r\n" +
                                            "                                                                    <a href=\"/HcmDashboard/DownloadAttachment?requestId="+ attachmentLogModel.Request + "&amp;vFile="+ attachmentLogModel.VFileName + "\" target=\"_blank\" class=\"download\">" + attachmentLogModel.FileName + "" + attachmentLogModel.FileExt + "</a>\r\n" +
                                            "                                                                </div>\r\n" +
                                            "                                                            </div>\r\n" +
                                            "                                                        </div>\r\n";
                        }
                    }
                    if (able)
                    {
                        uiRender += "                                            <div class=\"" + className + "\">\r\n" +
                                        "                                                <div class=\"timeline-badge\"></div>\r\n" +
                                        "                                                <div class=\"timeline-panel\">\r\n" +
                                        "                                                    <div class=\"timeline-heading\">\r\n" +
                                        heading +
                                        "                                                    </div>\r\n" +
                                        "                                                    <div class=\"timeline-body\">\r\n" +
                                        body +
                                        "                                                    </div>\r\n" +
                                        "                                                    <div class=\"timeline-footer d-flex align-items-center\">\r\n" +
                                        "                                                        <i class=\"mdi mdi-heart-outline text-muted mr-1 hide\"></i>\r\n" +
                                        "                                                        <span class=\"hide\">19</span>\r\n" +
                                        "                                                        <span class=\"ml-auto font-weight-bold\">" + nueRequestActivityModel.AddedOn.ToLocalTime() + "</span>\r\n" +
                                        "                                                    </div>\r\n" +
                                        "                                                </div>\r\n" +
                                        "                                            </div>\r\n";
                    }
                }
            }
            return uiRender;
        }
    }
}
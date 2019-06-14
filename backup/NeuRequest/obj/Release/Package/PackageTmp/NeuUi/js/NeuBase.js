﻿$(function () {
    showSwal = function (type) {
        'use strict';
        if (type === 'inter-approve-leave-cancelation-request') {
            swal({
                title: 'Are you sure?',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3f51b5',
                cancelButtonColor: '#ff4081',
                confirmButtonText: 'Great ',
                content: {
                    element: "input",
                    attributes: {
                        placeholder: "Approval Comment",
                        type: "textarea",
                        class: 'form-control',
                        id: 'subApprovalComment'
                    },
                },
                buttons: {
                    cancel: {
                        text: "Cancel",
                        value: null,
                        visible: true,
                        className: "btn btn-danger",
                        closeModal: true,
                    },
                    confirm: {
                        text: "OK",
                        value: true,
                        visible: true,
                        className: "btn btn-primary",
                        closeModal: true
                    }
                }
            }).then((value) => {
                if (value) {
                    var reqId = $('#requestId').val();
                    var usrId = $('#userId').val();
                    var formData = new FormData();
                    formData.append("requestId", "" + reqId + "");
                    formData.append("userId", "" + usrId + "");
                    formData.append("userComment", $('#subApprovalComment').val());
                    $.ajax({
                        type: "POST",
                        dataType: 'json',
                        enctype: 'multipart/form-data',
                        url: "/HcmDashboard/SubApproveLeaveCancelationRequest",
                        data: formData,
                        processData: false,
                        contentType: false,
                        cache: false,
                        timeout: 600000,
                        success: function (data) {
                            if (data.Status == 'Ok') {
                                swal({
                                    title: data.message,
                                    text: 'Data refresh in 2 sec.',
                                    timer: 2000,
                                    button: false
                                });
                                setTimeout(function () {
                                    location.reload();
                                }, 2000);
                            }
                            else {
                                swal({
                                    title: data.message,
                                    text: 'Data refresh in 4 sec.',
                                    timer: 4000,
                                    button: false
                                });

                                setTimeout(function () {
                                    location.reload();
                                }, 4000);
                            }
                        },
                        error: function (e) {
                            console.log("ERROR : ", e);
                        }
                    });
                }
            });

        }
        else if (type === 'inter-approve-leave-past-apply-request') {
            swal({
                title: 'Are you sure?',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3f51b5',
                cancelButtonColor: '#ff4081',
                confirmButtonText: 'Great ',
                content: {
                    element: "input",
                    attributes: {
                        placeholder: "Approval Comment",
                        type: "textarea",
                        class: 'form-control',
                        id: 'subApprovalComment'
                    },
                },
                buttons: {
                    cancel: {
                        text: "Cancel",
                        value: null,
                        visible: true,
                        className: "btn btn-danger",
                        closeModal: true,
                    },
                    confirm: {
                        text: "OK",
                        value: true,
                        visible: true,
                        className: "btn btn-primary",
                        closeModal: true
                    }
                }
            }).then((value) => {
                if (value) {
                    var reqId = $('#requestId').val();
                    var usrId = $('#userId').val();
                    var formData = new FormData();
                    formData.append("requestId", "" + reqId + "");
                    formData.append("userId", "" + usrId + "");
                    formData.append("userComment", $('#subApprovalComment').val());
                    $.ajax({
                        type: "POST",
                        dataType: 'json',
                        enctype: 'multipart/form-data',
                        url: "/HcmDashboard/SubApproveLeavePastApplyRequest",
                        data: formData,
                        processData: false,
                        contentType: false,
                        cache: false,
                        timeout: 600000,
                        success: function (data) {
                            if (data.Status == 'Ok') {
                                swal({
                                    title: data.message,
                                    text: 'Data refresh in 2 sec.',
                                    timer: 2000,
                                    button: false
                                });
                                setTimeout(function () {
                                    location.reload();
                                }, 2000);
                            }
                            else {
                                swal({
                                    title: data.message,
                                    text: 'Data refresh in 4 sec.',
                                    timer: 4000,
                                    button: false
                                });

                                setTimeout(function () {
                                    location.reload();
                                }, 4000);
                            }
                        },
                        error: function (e) {
                            console.log("ERROR : ", e);
                        }
                    });
                }
            });

        }
        else if (type === 'withdraw-leave-cancelation-request') {
            swal({
                title: 'Please withdrawal reason',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3f51b5',
                cancelButtonColor: '#ff4081',
                confirmButtonText: 'Great ',
                content: {
                    element: "input",
                    attributes: {
                        placeholder: "Feedback",
                        type: "textarea",
                        class: 'form-control',
                        id: 'withdrawalComment'
                    },
                },
                buttons: {
                    cancel: {
                        text: "Cancel",
                        value: null,
                        visible: true,
                        className: "btn btn-danger",
                        closeModal: true,
                    },
                    confirm: {
                        text: "OK",
                        value: true,
                        visible: true,
                        className: "btn btn-primary",
                        closeModal: true
                    }
                }
            }).then((value) => {
                if (value) {
                    var reqId = $('#requestId').val();
                    var usrId = $('#userId').val();
                    var formData = new FormData();
                    formData.append("requestId", "" + reqId + "");
                    formData.append("userId", "" + usrId + "");
                    formData.append("userComment", $('#withdrawalComment').val());
                    $.ajax({
                        type: "POST",
                        dataType: 'json',
                        enctype: 'multipart/form-data',
                        url: "/HcmDashboard/WithdrawLeaveCancelationRequest",
                        data: formData,
                        processData: false,
                        contentType: false,
                        cache: false,
                        timeout: 600000,
                        success: function (data) {
                            if (data.Status == 'Ok') {
                                swal({
                                    title: data.message,
                                    text: 'Data refresh in 2 sec.',
                                    timer: 2000,
                                    button: false
                                });
                                setTimeout(function () {
                                    location.reload();
                                }, 2000);
                            }
                            else {
                                swal({
                                    title: data.message,
                                    text: 'Data refresh in 4 sec.',
                                    timer: 4000,
                                    button: false
                                });

                                setTimeout(function () {
                                    location.reload();
                                }, 4000);
                            }
                        },
                        error: function (e) {
                            console.log("ERROR : ", e);
                        }
                    });
                }
            });
        }
        else if (type === 'withdraw-leave-past-apply-request') {
            swal({
                title: 'Please withdrawal reason',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3f51b5',
                cancelButtonColor: '#ff4081',
                confirmButtonText: 'Great ',
                content: {
                    element: "input",
                    attributes: {
                        placeholder: "Feedback",
                        type: "textarea",
                        class: 'form-control',
                        id: 'withdrawalComment'
                    },
                },
                buttons: {
                    cancel: {
                        text: "Cancel",
                        value: null,
                        visible: true,
                        className: "btn btn-danger",
                        closeModal: true,
                    },
                    confirm: {
                        text: "OK",
                        value: true,
                        visible: true,
                        className: "btn btn-primary",
                        closeModal: true
                    }
                }
            }).then((value) => {
                if (value) {
                    var reqId = $('#requestId').val();
                    var usrId = $('#userId').val();
                    var formData = new FormData();
                    formData.append("requestId", "" + reqId + "");
                    formData.append("userId", "" + usrId + "");
                    formData.append("userComment", $('#withdrawalComment').val());
                    $.ajax({
                        type: "POST",
                        dataType: 'json',
                        enctype: 'multipart/form-data',
                        url: "/HcmDashboard/WithdrawLeavePastApplyRequest",
                        data: formData,
                        processData: false,
                        contentType: false,
                        cache: false,
                        timeout: 600000,
                        success: function (data) {
                            if (data.Status == 'Ok') {
                                swal({
                                    title: data.message,
                                    text: 'Data refresh in 2 sec.',
                                    timer: 2000,
                                    button: false
                                });
                                setTimeout(function () {
                                    location.reload();
                                }, 2000);
                            }
                            else {
                                swal({
                                    title: data.message,
                                    text: 'Data refresh in 4 sec.',
                                    timer: 4000,
                                    button: false
                                });

                                setTimeout(function () {
                                    location.reload();
                                }, 4000);
                            }
                        },
                        error: function (e) {
                            console.log("ERROR : ", e);
                        }
                    });
                }
            });
        }
        else if (type === 'withdraw-leave-wfh-apply-request') {
            swal({
                title: 'Please withdrawal reason',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3f51b5',
                cancelButtonColor: '#ff4081',
                confirmButtonText: 'Great ',
                content: {
                    element: "input",
                    attributes: {
                        placeholder: "Feedback",
                        type: "textarea",
                        class: 'form-control',
                        id: 'withdrawalComment'
                    },
                },
                buttons: {
                    cancel: {
                        text: "Cancel",
                        value: null,
                        visible: true,
                        className: "btn btn-danger",
                        closeModal: true,
                    },
                    confirm: {
                        text: "OK",
                        value: true,
                        visible: true,
                        className: "btn btn-primary",
                        closeModal: true
                    }
                }
            }).then((value) => {
                if (value) {
                    var reqId = $('#requestId').val();
                    var usrId = $('#userId').val();
                    var formData = new FormData();
                    formData.append("requestId", "" + reqId + "");
                    formData.append("userId", "" + usrId + "");
                    formData.append("userComment", $('#withdrawalComment').val());
                    $.ajax({
                        type: "POST",
                        dataType: 'json',
                        enctype: 'multipart/form-data',
                        url: "/HcmDashboard/WithdrawLeaveWFHApplyRequest",
                        data: formData,
                        processData: false,
                        contentType: false,
                        cache: false,
                        timeout: 600000,
                        success: function (data) {
                            if (data.Status == 'Ok') {
                                swal({
                                    title: data.message,
                                    text: 'Data refresh in 2 sec.',
                                    timer: 2000,
                                    button: false
                                });
                                setTimeout(function () {
                                    location.reload();
                                }, 2000);
                            }
                            else {
                                swal({
                                    title: data.message,
                                    text: 'Data refresh in 4 sec.',
                                    timer: 4000,
                                    button: false
                                });

                                setTimeout(function () {
                                    location.reload();
                                }, 4000);
                            }
                        },
                        error: function (e) {
                            console.log("ERROR : ", e);
                        }
                    });
                }
            });
        }
        else if (type === 'withdraw-leave-bal-enq-request') {
            swal({
                title: 'Please withdrawal reason',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3f51b5',
                cancelButtonColor: '#ff4081',
                confirmButtonText: 'Great ',
                content: {
                    element: "input",
                    attributes: {
                        placeholder: "Feedback",
                        type: "textarea",
                        class: 'form-control',
                        id: 'withdrawalComment'
                    },
                },
                buttons: {
                    cancel: {
                        text: "Cancel",
                        value: null,
                        visible: true,
                        className: "btn btn-danger",
                        closeModal: true,
                    },
                    confirm: {
                        text: "OK",
                        value: true,
                        visible: true,
                        className: "btn btn-primary",
                        closeModal: true
                    }
                }
            }).then((value) => {
                if (value) {
                    var reqId = $('#requestId').val();
                    var usrId = $('#userId').val();
                    var formData = new FormData();
                    formData.append("requestId", "" + reqId + "");
                    formData.append("userId", "" + usrId + "");
                    formData.append("userComment", $('#withdrawalComment').val());
                    $.ajax({
                        type: "POST",
                        dataType: 'json',
                        enctype: 'multipart/form-data',
                        url: "/HcmDashboard/WithdrawLeaveBalanceEnqRequest",
                        data: formData,
                        processData: false,
                        contentType: false,
                        cache: false,
                        timeout: 600000,
                        success: function (data) {
                            if (data.Status == 'Ok') {
                                swal({
                                    title: data.message,
                                    text: 'Data refresh in 2 sec.',
                                    timer: 2000,
                                    button: false
                                });
                                setTimeout(function () {
                                    location.reload();
                                }, 2000);
                            }
                            else {
                                swal({
                                    title: data.message,
                                    text: 'Data refresh in 4 sec.',
                                    timer: 4000,
                                    button: false
                                });

                                setTimeout(function () {
                                    location.reload();
                                }, 4000);
                            }
                        },
                        error: function (e) {
                            console.log("ERROR : ", e);
                        }
                    });
                }
            });
        }
        else if (type === 'withdraw-hcm-address-proof-request') {
            swal({
                title: 'Please withdrawal reason',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3f51b5',
                cancelButtonColor: '#ff4081',
                confirmButtonText: 'Great ',
                content: {
                    element: "input",
                    attributes: {
                        placeholder: "Feedback",
                        type: "textarea",
                        class: 'form-control',
                        id: 'withdrawalComment'
                    },
                },
                buttons: {
                    cancel: {
                        text: "Cancel",
                        value: null,
                        visible: true,
                        className: "btn btn-danger",
                        closeModal: true,
                    },
                    confirm: {
                        text: "OK",
                        value: true,
                        visible: true,
                        className: "btn btn-primary",
                        closeModal: true
                    }
                }
            }).then((value) => {
                if (value) {
                    var reqId = $('#requestId').val();
                    var usrId = $('#userId').val();
                    var formData = new FormData();
                    formData.append("requestId", "" + reqId + "");
                    formData.append("userId", "" + usrId + "");
                    formData.append("userComment", $('#withdrawalComment').val());
                    $.ajax({
                        type: "POST",
                        dataType: 'json',
                        enctype: 'multipart/form-data',
                        url: "/HcmDashboard/WithdrawHCMAddressProofReqRequest",
                        data: formData,
                        processData: false,
                        contentType: false,
                        cache: false,
                        timeout: 600000,
                        success: function (data) {
                            if (data.Status == 'Ok') {
                                swal({
                                    title: data.message,
                                    text: 'Data refresh in 2 sec.',
                                    timer: 2000,
                                    button: false
                                });
                                setTimeout(function () {
                                    location.reload();
                                }, 2000);
                            }
                            else {
                                swal({
                                    title: data.message,
                                    text: 'Data refresh in 4 sec.',
                                    timer: 4000,
                                    button: false
                                });

                                setTimeout(function () {
                                    location.reload();
                                }, 4000);
                            }
                        },
                        error: function (e) {
                            console.log("ERROR : ", e);
                        }
                    });
                }
            });
        }
        else if (type === 'withdraw-hcm-employee-verification-request') {
            swal({
                title: 'Please withdrawal reason',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3f51b5',
                cancelButtonColor: '#ff4081',
                confirmButtonText: 'Great ',
                content: {
                    element: "input",
                    attributes: {
                        placeholder: "Feedback",
                        type: "textarea",
                        class: 'form-control',
                        id: 'withdrawalComment'
                    },
                },
                buttons: {
                    cancel: {
                        text: "Cancel",
                        value: null,
                        visible: true,
                        className: "btn btn-danger",
                        closeModal: true,
                    },
                    confirm: {
                        text: "OK",
                        value: true,
                        visible: true,
                        className: "btn btn-primary",
                        closeModal: true
                    }
                }
            }).then((value) => {
                if (value) {
                    var reqId = $('#requestId').val();
                    var usrId = $('#userId').val();
                    var formData = new FormData();
                    formData.append("requestId", "" + reqId + "");
                    formData.append("userId", "" + usrId + "");
                    formData.append("userComment", $('#withdrawalComment').val());
                    $.ajax({
                        type: "POST",
                        dataType: 'json',
                        enctype: 'multipart/form-data',
                        url: "/HcmDashboard/WithdrawHCMEmployeeVerificationRequest",
                        data: formData,
                        processData: false,
                        contentType: false,
                        cache: false,
                        timeout: 600000,
                        success: function (data) {
                            if (data.Status == 'Ok') {
                                swal({
                                    title: data.message,
                                    text: 'Data refresh in 2 sec.',
                                    timer: 2000,
                                    button: false
                                });
                                setTimeout(function () {
                                    location.reload();
                                }, 2000);
                            }
                            else {
                                swal({
                                    title: data.message,
                                    text: 'Data refresh in 4 sec.',
                                    timer: 4000,
                                    button: false
                                });

                                setTimeout(function () {
                                    location.reload();
                                }, 4000);
                            }
                        },
                        error: function (e) {
                            console.log("ERROR : ", e);
                        }
                    });
                }
            });
        }
        else if (type === 'close-leave-cancelation-request') {
            swal({
                title: 'Please provide feedback',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3f51b5',
                cancelButtonColor: '#ff4081',
                confirmButtonText: 'Great ',
                content: {
                    element: "input",
                    attributes: {
                        placeholder: "Feedback",
                        type: "textarea",
                        class: 'form-control',
                        id: 'FeedBackComment'
                    },
                },
                buttons: {
                    cancel: {
                        text: "Cancel",
                        value: null,
                        visible: true,
                        className: "btn btn-danger",
                        closeModal: true,
                    },
                    confirm: {
                        text: "OK",
                        value: true,
                        visible: true,
                        className: "btn btn-primary",
                        closeModal: true
                    }
                }
            }).then((value) => {
                if (value) {
                    var reqId = $('#requestId').val();
                    var usrId = $('#userId').val();
                    var formData = new FormData();
                    formData.append("requestId", "" + reqId + "");
                    formData.append("userId", "" + usrId + "");
                    formData.append("userComment", $('#FeedBackComment').val());
                    $.ajax({
                        type: "POST",
                        dataType: 'json',
                        enctype: 'multipart/form-data',
                        url: "/HcmDashboard/CloseLeaveCancelationRequest",
                        data: formData,
                        processData: false,
                        contentType: false,
                        cache: false,
                        timeout: 600000,
                        success: function (data) {
                            if (data.Status == 'Ok') {
                                swal({
                                    title: data.message,
                                    text: 'Data refresh in 2 sec.',
                                    timer: 2000,
                                    button: false
                                });
                                setTimeout(function () {
                                    location.reload();
                                }, 2000);
                            }
                            else {
                                swal({
                                    title: data.message,
                                    text: 'Data refresh in 4 sec.',
                                    timer: 4000,
                                    button: false
                                });

                                setTimeout(function () {
                                    location.reload();
                                }, 4000);
                            }
                        },
                        error: function (e) {
                            console.log("ERROR : ", e);
                        }
                    });
                }
            });
        }
        else if (type === 'close-leave-past-apply-request') {
            swal({
                title: 'Please provide feedback',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3f51b5',
                cancelButtonColor: '#ff4081',
                confirmButtonText: 'Great ',
                content: {
                    element: "input",
                    attributes: {
                        placeholder: "Feedback",
                        type: "textarea",
                        class: 'form-control',
                        id: 'FeedBackComment'
                    },
                },
                buttons: {
                    cancel: {
                        text: "Cancel",
                        value: null,
                        visible: true,
                        className: "btn btn-danger",
                        closeModal: true,
                    },
                    confirm: {
                        text: "OK",
                        value: true,
                        visible: true,
                        className: "btn btn-primary",
                        closeModal: true
                    }
                }
            }).then((value) => {
                if (value) {
                    var reqId = $('#requestId').val();
                    var usrId = $('#userId').val();
                    var formData = new FormData();
                    formData.append("requestId", "" + reqId + "");
                    formData.append("userId", "" + usrId + "");
                    formData.append("userComment", $('#FeedBackComment').val());
                    $.ajax({
                        type: "POST",
                        dataType: 'json',
                        enctype: 'multipart/form-data',
                        url: "/HcmDashboard/CloseLeavePastApplyRequest",
                        data: formData,
                        processData: false,
                        contentType: false,
                        cache: false,
                        timeout: 600000,
                        success: function (data) {
                            if (data.Status == 'Ok') {
                                swal({
                                    title: data.message,
                                    text: 'Data refresh in 2 sec.',
                                    timer: 2000,
                                    button: false
                                });
                                setTimeout(function () {
                                    location.reload();
                                }, 2000);
                            }
                            else {
                                swal({
                                    title: data.message,
                                    text: 'Data refresh in 4 sec.',
                                    timer: 4000,
                                    button: false
                                });

                                setTimeout(function () {
                                    location.reload();
                                }, 4000);
                            }
                        },
                        error: function (e) {
                            console.log("ERROR : ", e);
                        }
                    });
                }
            });
        }
        else if (type === 'close-leave-wfh-apply-request') {
            swal({
                title: 'Please provide feedback',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3f51b5',
                cancelButtonColor: '#ff4081',
                confirmButtonText: 'Great ',
                content: {
                    element: "input",
                    attributes: {
                        placeholder: "Feedback",
                        type: "textarea",
                        class: 'form-control',
                        id: 'FeedBackComment'
                    },
                },
                buttons: {
                    cancel: {
                        text: "Cancel",
                        value: null,
                        visible: true,
                        className: "btn btn-danger",
                        closeModal: true,
                    },
                    confirm: {
                        text: "OK",
                        value: true,
                        visible: true,
                        className: "btn btn-primary",
                        closeModal: true
                    }
                }
            }).then((value) => {
                if (value) {
                    var reqId = $('#requestId').val();
                    var usrId = $('#userId').val();
                    var formData = new FormData();
                    formData.append("requestId", "" + reqId + "");
                    formData.append("userId", "" + usrId + "");
                    formData.append("userComment", $('#FeedBackComment').val());
                    $.ajax({
                        type: "POST",
                        dataType: 'json',
                        enctype: 'multipart/form-data',
                        url: "/HcmDashboard/CloseWFHLeaveApplyRequest",
                        data: formData,
                        processData: false,
                        contentType: false,
                        cache: false,
                        timeout: 600000,
                        success: function (data) {
                            if (data.Status == 'Ok') {
                                swal({
                                    title: data.message,
                                    text: 'Data refresh in 2 sec.',
                                    timer: 2000,
                                    button: false
                                });
                                setTimeout(function () {
                                    location.reload();
                                }, 2000);
                            }
                            else {
                                swal({
                                    title: data.message,
                                    text: 'Data refresh in 4 sec.',
                                    timer: 4000,
                                    button: false
                                });

                                setTimeout(function () {
                                    location.reload();
                                }, 4000);
                            }
                        },
                        error: function (e) {
                            console.log("ERROR : ", e);
                        }
                    });
                }
            });
        }
        else if (type === 'close-leave-bal-enq-request') {
            swal({
                title: 'Please provide feedback',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3f51b5',
                cancelButtonColor: '#ff4081',
                confirmButtonText: 'Great ',
                content: {
                    element: "input",
                    attributes: {
                        placeholder: "Feedback",
                        type: "textarea",
                        class: 'form-control',
                        id: 'FeedBackComment'
                    },
                },
                buttons: {
                    cancel: {
                        text: "Cancel",
                        value: null,
                        visible: true,
                        className: "btn btn-danger",
                        closeModal: true,
                    },
                    confirm: {
                        text: "OK",
                        value: true,
                        visible: true,
                        className: "btn btn-primary",
                        closeModal: true
                    }
                }
            }).then((value) => {
                if (value) {
                    var reqId = $('#requestId').val();
                    var usrId = $('#userId').val();
                    var formData = new FormData();
                    formData.append("requestId", "" + reqId + "");
                    formData.append("userId", "" + usrId + "");
                    formData.append("userComment", $('#FeedBackComment').val());
                    $.ajax({
                        type: "POST",
                        dataType: 'json',
                        enctype: 'multipart/form-data',
                        url: "/HcmDashboard/CloseBalanceEnqLeaveRequest",
                        data: formData,
                        processData: false,
                        contentType: false,
                        cache: false,
                        timeout: 600000,
                        success: function (data) {
                            if (data.Status == 'Ok') {
                                swal({
                                    title: data.message,
                                    text: 'Data refresh in 2 sec.',
                                    timer: 2000,
                                    button: false
                                });
                                setTimeout(function () {
                                    location.reload();
                                }, 2000);
                            }
                            else {
                                swal({
                                    title: data.message,
                                    text: 'Data refresh in 4 sec.',
                                    timer: 4000,
                                    button: false
                                });

                                setTimeout(function () {
                                    location.reload();
                                }, 4000);
                            }
                        },
                        error: function (e) {
                            console.log("ERROR : ", e);
                        }
                    });
                }
            });
        }
        else if (type === 'close-hcm-address-proof-request') {
            swal({
                title: 'Please provide feedback',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3f51b5',
                cancelButtonColor: '#ff4081',
                confirmButtonText: 'Great ',
                content: {
                    element: "input",
                    attributes: {
                        placeholder: "Feedback",
                        type: "textarea",
                        class: 'form-control',
                        id: 'FeedBackComment'
                    },
                },
                buttons: {
                    cancel: {
                        text: "Cancel",
                        value: null,
                        visible: true,
                        className: "btn btn-danger",
                        closeModal: true,
                    },
                    confirm: {
                        text: "OK",
                        value: true,
                        visible: true,
                        className: "btn btn-primary",
                        closeModal: true
                    }
                }
            }).then((value) => {
                if (value) {
                    var reqId = $('#requestId').val();
                    var usrId = $('#userId').val();
                    var formData = new FormData();
                    formData.append("requestId", "" + reqId + "");
                    formData.append("userId", "" + usrId + "");
                    formData.append("userComment", $('#FeedBackComment').val());
                    $.ajax({
                        type: "POST",
                        dataType: 'json',
                        enctype: 'multipart/form-data',
                        url: "/HcmDashboard/CloseHCMAddressProofRequest",
                        data: formData,
                        processData: false,
                        contentType: false,
                        cache: false,
                        timeout: 600000,
                        success: function (data) {
                            if (data.Status == 'Ok') {
                                swal({
                                    title: data.message,
                                    text: 'Data refresh in 2 sec.',
                                    timer: 2000,
                                    button: false
                                });
                                setTimeout(function () {
                                    location.reload();
                                }, 2000);
                            }
                            else {
                                swal({
                                    title: data.message,
                                    text: 'Data refresh in 4 sec.',
                                    timer: 4000,
                                    button: false
                                });

                                setTimeout(function () {
                                    location.reload();
                                }, 4000);
                            }
                        },
                        error: function (e) {
                            console.log("ERROR : ", e);
                        }
                    });
                }
            });
        }
        else if (type === 'close-hcm-employee-verification-request') {
            swal({
                title: 'Please provide feedback',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3f51b5',
                cancelButtonColor: '#ff4081',
                confirmButtonText: 'Great ',
                content: {
                    element: "input",
                    attributes: {
                        placeholder: "Feedback",
                        type: "textarea",
                        class: 'form-control',
                        id: 'FeedBackComment'
                    },
                },
                buttons: {
                    cancel: {
                        text: "Cancel",
                        value: null,
                        visible: true,
                        className: "btn btn-danger",
                        closeModal: true,
                    },
                    confirm: {
                        text: "OK",
                        value: true,
                        visible: true,
                        className: "btn btn-primary",
                        closeModal: true
                    }
                }
            }).then((value) => {
                if (value) {
                    var reqId = $('#requestId').val();
                    var usrId = $('#userId').val();
                    var formData = new FormData();
                    formData.append("requestId", "" + reqId + "");
                    formData.append("userId", "" + usrId + "");
                    formData.append("userComment", $('#FeedBackComment').val());
                    $.ajax({
                        type: "POST",
                        dataType: 'json',
                        enctype: 'multipart/form-data',
                        url: "/HcmDashboard/CloseHCMEmployeeVerificationRequest",
                        data: formData,
                        processData: false,
                        contentType: false,
                        cache: false,
                        timeout: 600000,
                        success: function (data) {
                            if (data.Status == 'Ok') {
                                swal({
                                    title: data.message,
                                    text: 'Data refresh in 2 sec.',
                                    timer: 2000,
                                    button: false
                                });
                                setTimeout(function () {
                                    location.reload();
                                }, 2000);
                            }
                            else {
                                swal({
                                    title: data.message,
                                    text: 'Data refresh in 4 sec.',
                                    timer: 4000,
                                    button: false
                                });

                                setTimeout(function () {
                                    location.reload();
                                }, 4000);
                            }
                        },
                        error: function (e) {
                            console.log("ERROR : ", e);
                        }
                    });
                }
            });
        }
        else if (type === 'final-approve-leave-cancelation-request') {
            swal({
                title: 'Are you sure?',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3f51b5',
                cancelButtonColor: '#ff4081',
                confirmButtonText: 'Great ',
                content: {
                    element: "input",
                    attributes: {
                        placeholder: "Approval Comment",
                        type: "textarea",
                        class: 'form-control',
                        id: 'ApprovalComment'
                    },
                },
                buttons: {
                    cancel: {
                        text: "Cancel",
                        value: null,
                        visible: true,
                        className: "btn btn-danger",
                        closeModal: true,
                    },
                    confirm: {
                        text: "OK",
                        value: true,
                        visible: true,
                        className: "btn btn-primary",
                        closeModal: true
                    }
                }
            }).then((value) => {
                if (value) {
                    var reqId = $('#requestId').val();
                    var usrId = $('#userId').val();
                    var formData = new FormData();
                    formData.append("requestId", "" + reqId + "");
                    formData.append("userId", "" + usrId + "");
                    formData.append("userComment", $('#ApprovalComment').val());
                    $.ajax({
                        type: "POST",
                        dataType: 'json',
                        enctype: 'multipart/form-data',
                        url: "/HcmDashboard/ApproveLeaveCancelationRequest",
                        data: formData,
                        processData: false,
                        contentType: false,
                        cache: false,
                        timeout: 600000,
                        success: function (data) {
                            if (data.Status == 'Ok') {
                                swal({
                                    title: data.message,
                                    text: 'Data refresh in 2 sec.',
                                    timer: 2000,
                                    button: false
                                });
                                setTimeout(function () {
                                    location.reload();
                                }, 2000);
                            }
                            else {
                                swal({
                                    title: data.message,
                                    text: 'Data refresh in 4 sec.',
                                    timer: 4000,
                                    button: false
                                });

                                setTimeout(function () {
                                    location.reload();
                                }, 4000);
                            }
                        },
                        error: function (e) {
                            console.log("ERROR : ", e);
                        }
                    });
                }
            });
        }
        else if (type === 'final-approve-leave-past-apply-request') {
            swal({
                title: 'Are you sure?',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3f51b5',
                cancelButtonColor: '#ff4081',
                confirmButtonText: 'Great ',
                content: {
                    element: "input",
                    attributes: {
                        placeholder: "Approval Comment",
                        type: "textarea",
                        class: 'form-control',
                        id: 'ApprovalComment'
                    },
                },
                buttons: {
                    cancel: {
                        text: "Cancel",
                        value: null,
                        visible: true,
                        className: "btn btn-danger",
                        closeModal: true,
                    },
                    confirm: {
                        text: "OK",
                        value: true,
                        visible: true,
                        className: "btn btn-primary",
                        closeModal: true
                    }
                }
            }).then((value) => {
                if (value) {
                    var reqId = $('#requestId').val();
                    var usrId = $('#userId').val();
                    var formData = new FormData();
                    formData.append("requestId", "" + reqId + "");
                    formData.append("userId", "" + usrId + "");
                    formData.append("userComment", $('#ApprovalComment').val());
                    $.ajax({
                        type: "POST",
                        dataType: 'json',
                        enctype: 'multipart/form-data',
                        url: "/HcmDashboard/ApproveLeavePastApplyRequest",
                        data: formData,
                        processData: false,
                        contentType: false,
                        cache: false,
                        timeout: 600000,
                        success: function (data) {
                            if (data.Status == 'Ok') {
                                swal({
                                    title: data.message,
                                    text: 'Data refresh in 2 sec.',
                                    timer: 2000,
                                    button: false
                                });
                                setTimeout(function () {
                                    location.reload();
                                }, 2000);
                            }
                            else {
                                swal({
                                    title: data.message,
                                    text: 'Data refresh in 4 sec.',
                                    timer: 4000,
                                    button: false
                                });

                                setTimeout(function () {
                                    location.reload();
                                }, 4000);
                            }
                        },
                        error: function (e) {
                            console.log("ERROR : ", e);
                        }
                    });
                }
            });
        }
        else if (type === 'final-approve-leave-wfh-apply-request') {
            swal({
                title: 'Are you sure?',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3f51b5',
                cancelButtonColor: '#ff4081',
                confirmButtonText: 'Great ',
                content: {
                    element: "input",
                    attributes: {
                        placeholder: "Approval Comment",
                        type: "textarea",
                        class: 'form-control',
                        id: 'ApprovalComment'
                    },
                },
                buttons: {
                    cancel: {
                        text: "Cancel",
                        value: null,
                        visible: true,
                        className: "btn btn-danger",
                        closeModal: true,
                    },
                    confirm: {
                        text: "OK",
                        value: true,
                        visible: true,
                        className: "btn btn-primary",
                        closeModal: true
                    }
                }
            }).then((value) => {
                if (value) {
                    var reqId = $('#requestId').val();
                    var usrId = $('#userId').val();
                    var formData = new FormData();
                    formData.append("requestId", "" + reqId + "");
                    formData.append("userId", "" + usrId + "");
                    formData.append("userComment", $('#ApprovalComment').val());
                    $.ajax({
                        type: "POST",
                        dataType: 'json',
                        enctype: 'multipart/form-data',
                        url: "/HcmDashboard/ApproveWFHLeaveRequest",
                        data: formData,
                        processData: false,
                        contentType: false,
                        cache: false,
                        timeout: 600000,
                        success: function (data) {
                            if (data.Status == 'Ok') {
                                swal({
                                    title: data.message,
                                    text: 'Data refresh in 2 sec.',
                                    timer: 2000,
                                    button: false
                                });
                                setTimeout(function () {
                                    location.reload();
                                }, 2000);
                            }
                            else {
                                swal({
                                    title: data.message,
                                    text: 'Data refresh in 4 sec.',
                                    timer: 4000,
                                    button: false
                                });

                                setTimeout(function () {
                                    location.reload();
                                }, 4000);
                            }
                        },
                        error: function (e) {
                            console.log("ERROR : ", e);
                        }
                    });
                }
            });
        }
        else if (type === 'final-approve-leave-bal-enq-request') {
            swal({
                title: 'Are you sure?',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3f51b5',
                cancelButtonColor: '#ff4081',
                confirmButtonText: 'Great ',
                content: {
                    element: "input",
                    attributes: {
                        placeholder: "Approval Comment",
                        type: "textarea",
                        class: 'form-control',
                        id: 'ApprovalComment'
                    },
                },
                buttons: {
                    cancel: {
                        text: "Cancel",
                        value: null,
                        visible: true,
                        className: "btn btn-danger",
                        closeModal: true,
                    },
                    confirm: {
                        text: "OK",
                        value: true,
                        visible: true,
                        className: "btn btn-primary",
                        closeModal: true
                    }
                }
            }).then((value) => {
                if (value) {
                    var reqId = $('#requestId').val();
                    var usrId = $('#userId').val();
                    var formData = new FormData();
                    formData.append("requestId", "" + reqId + "");
                    formData.append("userId", "" + usrId + "");
                    formData.append("userComment", $('#ApprovalComment').val());
                    $.ajax({
                        type: "POST",
                        dataType: 'json',
                        enctype: 'multipart/form-data',
                        url: "/HcmDashboard/ApproveBalanceEnqLeaveRequest",
                        data: formData,
                        processData: false,
                        contentType: false,
                        cache: false,
                        timeout: 600000,
                        success: function (data) {
                            if (data.Status == 'Ok') {
                                swal({
                                    title: data.message,
                                    text: 'Data refresh in 2 sec.',
                                    timer: 2000,
                                    button: false
                                });
                                setTimeout(function () {
                                    location.reload();
                                }, 2000);
                            }
                            else {
                                swal({
                                    title: data.message,
                                    text: 'Data refresh in 4 sec.',
                                    timer: 4000,
                                    button: false
                                });

                                setTimeout(function () {
                                    location.reload();
                                }, 4000);
                            }
                        },
                        error: function (e) {
                            console.log("ERROR : ", e);
                        }
                    });
                }
            });
        }
        else if (type === 'final-hcm-address-proof-request') {
            swal({
                title: 'Are you sure?',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3f51b5',
                cancelButtonColor: '#ff4081',
                confirmButtonText: 'Great ',
                content: {
                    element: "input",
                    attributes: {
                        placeholder: "Approval Comment",
                        type: "textarea",
                        class: 'form-control',
                        id: 'ApprovalComment'
                    },
                },
                buttons: {
                    cancel: {
                        text: "Cancel",
                        value: null,
                        visible: true,
                        className: "btn btn-danger",
                        closeModal: true,
                    },
                    confirm: {
                        text: "OK",
                        value: true,
                        visible: true,
                        className: "btn btn-primary",
                        closeModal: true
                    }
                }
            }).then((value) => {
                if (value) {
                    var reqId = $('#requestId').val();
                    var usrId = $('#userId').val();
                    var formData = new FormData();
                    formData.append("requestId", "" + reqId + "");
                    formData.append("userId", "" + usrId + "");
                    formData.append("userComment", $('#ApprovalComment').val());
                    $.ajax({
                        type: "POST",
                        dataType: 'json',
                        enctype: 'multipart/form-data',
                        url: "/HcmDashboard/ApproveHCMAddressProofRequest",
                        data: formData,
                        processData: false,
                        contentType: false,
                        cache: false,
                        timeout: 600000,
                        success: function (data) {
                            if (data.Status == 'Ok') {
                                swal({
                                    title: data.message,
                                    text: 'Data refresh in 2 sec.',
                                    timer: 2000,
                                    button: false
                                });
                                setTimeout(function () {
                                    location.reload();
                                }, 2000);
                            }
                            else {
                                swal({
                                    title: data.message,
                                    text: 'Data refresh in 4 sec.',
                                    timer: 4000,
                                    button: false
                                });

                                setTimeout(function () {
                                    location.reload();
                                }, 4000);
                            }
                        },
                        error: function (e) {
                            console.log("ERROR : ", e);
                        }
                    });
                }
            });
        }
        else if (type === 'final-hcm-employee-verification-request') {
            swal({
                title: 'Are you sure?',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3f51b5',
                cancelButtonColor: '#ff4081',
                confirmButtonText: 'Great ',
                content: {
                    element: "input",
                    attributes: {
                        placeholder: "Approval Comment",
                        type: "textarea",
                        class: 'form-control',
                        id: 'ApprovalComment'
                    },
                },
                buttons: {
                    cancel: {
                        text: "Cancel",
                        value: null,
                        visible: true,
                        className: "btn btn-danger",
                        closeModal: true,
                    },
                    confirm: {
                        text: "OK",
                        value: true,
                        visible: true,
                        className: "btn btn-primary",
                        closeModal: true
                    }
                }
            }).then((value) => {
                if (value) {
                    var reqId = $('#requestId').val();
                    var usrId = $('#userId').val();
                    var formData = new FormData();
                    formData.append("requestId", "" + reqId + "");
                    formData.append("userId", "" + usrId + "");
                    formData.append("userComment", $('#ApprovalComment').val());
                    $.ajax({
                        type: "POST",
                        dataType: 'json',
                        enctype: 'multipart/form-data',
                        url: "/HcmDashboard/ApproveHCMEmployeeVerificationRequest",
                        data: formData,
                        processData: false,
                        contentType: false,
                        cache: false,
                        timeout: 600000,
                        success: function (data) {
                            if (data.Status == 'Ok') {
                                swal({
                                    title: data.message,
                                    text: 'Data refresh in 2 sec.',
                                    timer: 2000,
                                    button: false
                                });
                                setTimeout(function () {
                                    location.reload();
                                }, 2000);
                            }
                            else {
                                swal({
                                    title: data.message,
                                    text: 'Data refresh in 4 sec.',
                                    timer: 4000,
                                    button: false
                                });

                                setTimeout(function () {
                                    location.reload();
                                }, 4000);
                            }
                        },
                        error: function (e) {
                            console.log("ERROR : ", e);
                        }
                    });
                }
            });
        }

















    }
});
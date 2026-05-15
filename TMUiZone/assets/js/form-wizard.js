var FormWizard = function () {
    var wizardContent = $('#wizard');

    var wizardForm = $('#form');

    var initWizard = function () {
        // function to initiate Wizard Form
        wizardContent.smartWizard({
            selected: 0,
            keyNavigation: false,
            onLeaveStep: leaveAStepCallback,
            onShowStep: onShowStep,
        });
        var numberOfSteps = 0;
        animateBar();
        initValidator();
    };

    var animateBar = function (val) {
        if ((typeof val == 'undefined') || val == "") {
            val = 1;
        };
        numberOfSteps = $('.swMain > ul > li').length;
        var valueNow = Math.floor(100 / numberOfSteps * val);
        $('.step-bar').css('width', valueNow + '%');
    };


    var initValidator = function () {
        $.validator.addMethod("cardExpiry", function () {
            //if all values are selected
            if ($("#card_expiry_mm").val() != "" && $("#card_expiry_yyyy").val() != "") {
                return true;
            } else {
                return false;
            }
        }, 'Please select a month and year');

        $.validator.setDefaults({
            errorElement: "span", // contain the error msg in a span tag
            errorClass: 'help-block',
            errorPlacement: function (error, element) {// render error placement for each input type
                if (element.attr("type") == "radio" || element.attr("type") == "checkbox") {// for chosen elements, need to insert the error after the chosen container
                    error.insertAfter($(element).closest('.form-group').children('div').children().last());
                } else if (element.attr("name") == "card_expiry_mm" || element.attr("name") == "card_expiry_yyyy") {
                    error.appendTo($(element).closest('.form-group').children('div'));
                } else {
                    error.insertAfter(element);
                    // for other inputs, just perform default behavior
                }
            },
            ignore: ':hidden',
            rules: {
                nntxtStudentName: {
                    minlength: 1,
                    required: true
                },
                txtStudentName: {
                    minlength: 1,
                    required: true
                },
                //txtDOJClassStartdate: {
                //    required: true,
                //    date: true
                //},
                txtStudentMobileNumberPresent: {
                    minlength: 10,
                    required: true,
                    number: true
                },
                txtStudentMobileNumberManipal: {
                    minlength: 10,
                    required: true,
                    number: true
                    
                },
                txtStudentEmailID: {
                    required: true,
                    email: true,
                },
                
                txtNationality: {
                    required: true
                },
                txtMotherTongue: {
                    required: true
                },
                stuMaritalStatus: {
                    required: true
                },
                
                txtPANCardNumber: {
                    required: true,
                    minlength: 10
                },
                txtAadharCardNumber: {
                    minlength: 12,
                    number: true
                },

                txtBankACNumber: {
                    minlength:10,
                    required: true,
                    number: true
                },
                ntxtBankACNumber: {
                    minlength: 5,
                    number: true
                },
                txtAccountHolderName: {
                    minlength: 2,
                    required: true

                },
                txtIFSCCode: {
                    minlength: 11,
                    required: true
                },
                ntxtIFSCCode: {
                    minlength: 5
                },

                txtBranch: {
                    minlength: 1,
                    required: true
                },
                txtBankName: {
                    minlength: 1,
                    required: true
                },
                txtPassPortNumber: {
                    minlength: 1,
                    required: true
                },
                txtPassPortIssuedDate: {
                    minlength: 1,
                    required: true,
                    date: true
                },
                txtPassPortExpiryDate: {
                    minlength: 1,
                    required: true,
                    date: true
                },
                txtVisaNumber: {
                    minlength: 1,
                    required: true
                },
                txtVisaIssuedDate: {
                    minlength: 1,
                    required: true,
                    date: true
                },
                txtVisaExpiryDate: {
                    minlength: 1,
                    required: true,
                    date: true
                },
                txtRCRPNumber: {
                    minlength: 1,
                    required: true
                },
                txtRCRPIssuedDate: {
                    minlength: 1,
                    required: true,
                    date: true
                },
                txtRCRPExpiryDate: {
                    minlength: 1,
                    required: true,
                    date: true
                },
                txtSFormID: {
                    minlength: 1,
                    required: true
                },
                txtPFatherName: {
                    minlength: 1,
                    required: true
                },
                txtFatherOccupation: {
                    minlength: 1,
                    required: true
                },
                txtFatherContactNumber: {
                    minlength: 10,
                    required: true,
                    number: true
                },
                ntxtFatherContactNumber: {
                    minlength: 10,
                    number: true
                },
                txtFatherEmailID: {
                    required: true,
                    email: true
                },
                txtPMotherName: {
                    minlength: 1,
                    required: true
                },
                txtMotherOccupation: {
                    minlength: 1,
                    required: true
                },
                txtMotherContactNumber: {
                    minlength: 10,
                    required: true,
                    number: true
                },
                ntxtMotherContactNumber: {
                    minlength: 10,
                    number: true
                },
                txtPMotherEmailID: {
                    required: true,
                    email: true
                },
                txtGuardianName: {
                    minlength: 1,
                    required: true
                },
                txtGuardianOccupation: {
                    minlength: 1,
                    required: true
                },
                txtGuardianEmailID: {
                    required: true,
                    email: true
                },
                txtGuardianContactNumber: {
                    minlength: 10,
                    required: true,
                    number: true
                },

                ntxtGuardianContactNumber: {
                    minlength: 10,
                    number: true
                },
                
                //txtOfficialCorrespondenceEmail: {
                //    required: true,
                //    email: true
                //},
                //txtOfficialCorrespondenceMobileNumber: {
                //    minlength: 10,
                //    required: true,
                //    number: true
                //},
                //txtEmergencyContactNumber: {
                //    minlength: 10,
                //    required: true,
                //    number: true
                //},
                //tab 2
                txtQualifyingBoardUniversity: {
                    minlength: 1,
                    required: true

                },
                txtCollegeLastStudied: {
                    minlength: 1,
                    required: true
                },
                //txtQualifyingExam: {
                //    minlength: 3,
                //    required: true,
                //},
                txtQualifyingYearPass: {
                    minlength: 1,
                    required: true,
                    number: true
                },
                txtExamMarksGradesPoints: {
                    required: true
                },
                txtEntranceTestMarks: {
                    minlength: 2,
                    required: true,
                    number: true
                },
                txt10th: {
                    required: true
                   
                },
                txtPhysicsMathsOptional: {
                    minlength: 2,
                    required: true,
                    number: true
                },
                txtOptionalSubjectName: {
                    required: true
                },
                // Adress validation

                txtPermanentAddressLine1: {
                    minlength: 2,
                    required: true
                },
                txtPermanentAddressLine2: {
                    minlength: 2,
                    required: true
                },
                txtPermanentAddressLine3: {
                    minlength: 2,
                    required: true
                },
                txtPermanentAddressLine3: {
                    minlength: 2,
                    required: true
                },
                txtPlace: {
                    minlength: 2,
                    required: true
                },
                txtState: {
                    minlength: 2,
                    required: true
                },
                txtCountry: {
                    minlength: 2,
                    required: true
                },
                txtPincode: {
                    minlength: 6,
                    required: true
                },
                txtPresentAddressLine1: {
                    minlength: 2,
                    required: true
                },
                txtPresentAddressLine2: {
                    minlength: 2,
                    required: true
                },
                txtPresentAddressLine3: {
                    minlength: 2,
                    required: true
                },
                txtPresentPlace: {
                    minlength: 2,
                    required: true
                },
                txtPresentState: {
                    minlength: 2,
                    required: true
                },
                txtPresentCountry: {
                    minlength: 2,
                    required: true
                },
                txtPresentPincode: {
                    minlength: 6,
                    required: true,
                    number: true
                },
                //txtHostelAllottedOn: {
                //    date: true
                //},
                //txtHostelVacatedOn: {
                //    date: true
                //},

                txtNumberOfCreditsEarned: {
                    number: true
                },
                txtSName: {
                    required: true,
                    minlength: 1
                },
                txtSRelation: {
                    required: true,
                    minlength: 1
                },
                txtSAddressLine1: {
                    required: true,
                    minlength: 1
                },
                txtSAddressLine2: {
                    required: true,
                    minlength: 1
                },
                txtSAddressLine3: {
                    required: true,
                    minlength: 1
                },
                txtSPlace: {
                    required: true,
                    minlength: 1
                },
                txtSPinCode: {
                    required: true,
                    minlength: 6
                },
                ntxtSPinCode: {
                    minlength: 6,
                    number:true
                },
                //txtIfYesAdmissionToWhichSemester: {
                //    required: true,
                //    minlength: 1
                //},
                //txtHostelContactNumber: {
                //    minlength: 10
                //},

                username: {
                    minlength: 2,
                    required: true
                },
                email: {
                    required: true,
                    email: true
                },
                password: {
                    minlength: 6,
                    required: true
                },
                password_again: {
                    required: true,
                    minlength: 5,
                    equalTo: "#password"
                },
                full_name: {
                    required: true,
                    minlength: 2,
                },
                phone: {
                    required: true,
                    number: true
                },
                gender: {
                    required: true
                },
                address: {
                    required: true
                },
                city: {
                    required: true
                },
                country: {
                    required: true
                },
                card_name: {
                    required: true
                },
                card_number: {
                    minlength: 16,
                    maxlength: 16,
                    required: true
                },
                card_cvc: {
                    digits: true,
                    required: true,
                    minlength: 3,
                    maxlength: 4
                },
                card_expiry_yyyy: "cardExpiry",
                payment: {
                    required: true,
                    minlength: 1
                }
            },
            messages: {
                firstname: "Please specify your first name"
            },
            highlight: function (element) {
                $(element).closest('.help-block').removeClass('valid');
                // display OK icon
                $(element).closest('.form-group').removeClass('has-success').addClass('has-error').find('.symbol').removeClass('ok').addClass('required');
                // add the Bootstrap error class to the control group
            },
            unhighlight: function (element) {// revert the change done by hightlight
                $(element).closest('.form-group').removeClass('has-error');
                // set error class to the control group
            },
            success: function (label, element) {
                label.addClass('help-block valid');
                // mark the current input as valid and display OK icon
                $(element).closest('.form-group').removeClass('has-error').addClass('has-success').find('.symbol').removeClass('required').addClass('ok');
            }
        });
    };

    var displayConfirm = function () {
        $('.display-value', form).each(function () {
            var input = $('[name="' + $(this).attr("data-display") + '"]', form);
            if (input.attr("type") == "text" || input.attr("type") == "email" || input.is("textarea")) {
                $(this).html(input.val());
            } else if (input.is("select")) {
                $(this).html(input.find('option:selected').text());
            } else if (input.is(":radio") || input.is(":checkbox")) {

                $(this).html(input.filter(":checked").closest('label').text());
            } else if ($(this).attr("data-display") == 'card_expiry') {
                $(this).html($('[name="card_expiry_mm"]', form).val() + '/' + $('[name="card_expiry_yyyy"]', form).val());
            }
        });
    };

    var onShowStep = function (obj, context) {
        $(".next-step").unbind("click").click(function (e) {
            e.preventDefault();
            wizardContent.smartWizard("goForward");
        });
        $(".back-step").unbind("click").click(function (e) {
            e.preventDefault();
            wizardContent.smartWizard("goBackward");
        });
        $(".finish-step").unbind("click").click(function (e) {
            e.preventDefault();
            onFinish(obj, context);
        });
    };

    var leaveAStepCallback = function (obj, context) {
        return validateSteps(context.fromStep, context.toStep);
        // return false to stay on step and true to continue navigation
    };

    var onFinish = function (obj, context) {
        if (validateAllSteps()) {
            alert('form submit function');
            $('.anchor').children("li").last().children("a").removeClass('wait').removeClass('selected').addClass('done');
            //wizardForm.submit();
        }
    };

    var validateSteps = function (stepnumber, nextstep) {
        var isStepValid = false;
        if (numberOfSteps > nextstep && nextstep > stepnumber) {
            // cache the form element selector
            if (wizardForm.valid()) {// validate the form
                wizardForm.validate().focusInvalid();
                $('.anchor').children("li:nth-child(" + stepnumber + ")").children("a").removeClass('wait');
                //focus the invalid fields
                animateBar(nextstep);
                isStepValid = true;
                return true;
            };
        } else if (nextstep < stepnumber) {
            $('.anchor').children("li:nth-child(" + stepnumber + ")").children("a").addClass('wait');
            animateBar(nextstep);
            return true;
        } else {
            if (wizardForm.valid()) {
                $('.anchor').children("li:nth-child(" + stepnumber + ")").children("a").removeClass('wait');
                displayConfirm();
                animateBar(nextstep);
                return true;
            }
        }
        ;
    };

    var validateAllSteps = function () {
        var isStepValid = true;
        // all step validation logic
        return isStepValid;
    };

    return {
        init: function () {
            initWizard();
        }
    };
}();


$(document).ready(function () {
    var baseUrl = $("base").first().attr("href");
    var Warranty_Code = [];
    var Warranty_Group = [];
    var Project_main = [];
    var Customer_main = [];
    var Sale_main = [];
    var HR_main =[];
    var Sale_Group_main = [];
    var Project_main2 = [];

    // start Warranty_Code
    $.ajax({
        url: baseUrl + "RE_Master/CS_Warranty_Code_ReadList",
        data: { id: 1 },
        dataType: "json",
        success: function (war_group_code) {
            $.each(war_group_code.data, function (idx, obj) {
                Warranty_Code.push({ value: obj.war_code, label: obj.remark });
            });

            $(".war_group_code_autocomplete").autocomplete({
                source: Warranty_Code,
                focus: function (event, ui) {
                    event.preventDefault();
                    $(this).val(ui.item.label);
                    $(this).attr("name");
                },
                select: function (event, ui) {
                    event.preventDefault();
                    $(this).val(ui.item.label);
                    $("." + $(this).attr('form_fix')).val(ui.item.value);
                }
            });
            
        }
    });   // end  Warranty_Code

  
    // start  Warranty Group 
    $.ajax({
        url: baseUrl + "RE_Master/CS_Warranty_Group_ReadList",
        data: { id: 1 },
        dataType: "json",
        success: function (war_group) {
            $.each(war_group.data, function (idx, obj) {
                Warranty_Group.push({ value: obj.war_group_code, label: obj.war_group_des });
            });

            $(".war_group_autocomplete").autocomplete({
                source: Warranty_Group,
                focus: function (event, ui) {
                    event.preventDefault();
                    $(this).val(ui.item.label);
                },
                select: function (event, ui) {
                    event.preventDefault();
                    $(this).val(ui.item.label);
                    $("." + $(this).attr('form_fix')).val(ui.item.value);
                }
            });


        }
    }); // end  Warranty Group



    // start  Project_main 
    $.ajax({
        url: baseUrl + "Master/Master_Project_Main_ReadList",
        data: { id: 1 },
        dataType: "json",
        success: function (project) {
            $.each(project.data, function (idx, obj) {
                Project_main.push({ value: obj.pre_event, label: obj.pre_des });
            });

            $(".pre_des_autocomplete").autocomplete({
                source: Project_main,
                focus: function (event, ui) {
                    event.preventDefault();
                    $(this).val(ui.item.label);
                },
                select: function (event, ui) {
                    event.preventDefault();
                    $(this).val(ui.item.label);
                    $("." + $(this).attr('form_fix')).val(ui.item.value);
                    $(".QuesH_spre_event").val(ui.item.value).trigger('click');
                    $('.Q_disabled').prop("disabled", false);
                    //$("#spre_event").click();
                }
            });


        }
    }); // end  Project_main



    // start  customer_main 
    $.ajax({
        url: baseUrl + "customer/customer_readlist",
        data: { id: 1 },
        dataType: "json",
        success: function (customer) {
            $.each(customer.data, function (idx, obj) {
                Customer_main.push({ value: obj.customer_code, label: obj.name_th });
            });

            $(".customer_name_autocomplete").autocomplete({
                source: Customer_main,
                focus: function (event, ui) {
                    event.preventDefault();
                    $(this).val(ui.item.label);
                },
                select: function (event, ui) {
                    event.preventDefault();
                    $(this).val(ui.item.label);
                    $("." + $(this).attr('form_fix')).val(ui.item.value);
                }
            });


        }
    }); // end  customer_main


    // start  Sale_main 
    $.ajax({
        url: baseUrl + "RE_Transaction/SaleAll_Readlist",
        data: { id: 1 },
        dataType: "json",
        success: function (sale) {
            $.each(sale.data, function (idx, obj) {
                Sale_main.push({ value: obj.salecode, label: obj.salename });
            });

            $(".salename_autocomplete").autocomplete({
                source: Sale_main,
                focus: function (event, ui) {
                    event.preventDefault();
                    $(this).val(ui.item.label);
                },
                select: function (event, ui) {
                    event.preventDefault();
                    $(this).val(ui.item.label);
                    $("." + $(this).attr('form_fix')).val(ui.item.value);
                }
            });


        }
    }); // end  Sale_main

 // start  HR_main(select empno) 
    $.ajax({
        url: baseUrl + "se/hr_emp_read",
        data: { id: 1 },
        dataType: "json",
        success: function (hr_emp) {
            $.each(hr_emp.data, function (idx, obj) {
                HR_main.push({ value: obj.empno, label: obj.empname_t });
            });

            $(".empno_autocomplete").autocomplete({
                source:  HR_main,
                focus: function (event, ui) {
                    event.preventDefault();
                    $(this).val(ui.item.label);
                },
                select: function (event, ui) {
                    event.preventDefault();
                    $(this).val(ui.item.label);
                    $("." + $(this).attr('form_fix')).val(ui.item.value);
                }
            });
        }
    }); // end  hr_emp_main(select empno)

// start  Sale_Group_main(select Group_Sale) 
    $.ajax({
        url: baseUrl + "re_master/SaleGroup_Readlist",
        data: { id: 1 },
        dataType: "json",
        success: function (sm_group_sale) {
            $.each(sm_group_sale.data, function (idx, obj) {
                Sale_Group_main.push({ value: obj.group_code, label: obj.group_name });
            });

            $(".salegroup_autocomplete").autocomplete({
                source:  Sale_Group_main,
                focus: function (event, ui) {
                    event.preventDefault();
                    $(this).val(ui.item.label);
                },
                select: function (event, ui) {
                    event.preventDefault();
                    $(this).val(ui.item.label);
                    $("." + $(this).attr('form_fix')).val(ui.item.value);
                }
            });
        }
    }); // end  hr_emp_main(select empno)

 // start  Project_main2  output pre_event2 
    $.ajax({
        url: baseUrl + "Master/Master_Project_Main_ReadList",
        data: { id: 1 },
        dataType: "json",
        success: function (project2) {
            $.each(project2.data, function (idx, obj) {
                Project_main2.push({ value: obj.pre_event2,label: obj.pre_des });
            });

            $(".pre_event2_autocomplete").autocomplete({
                source: Project_main2,
                focus: function (event, ui) {
                    event.preventDefault();
                    $(this).val(ui.item.label);
                },
                select: function (event, ui) {
                    event.preventDefault();
                    $(this).val(ui.item.label);
                    $("." + $(this).attr('form_fix')).val(ui.item.value);
                }
            });


        }
    }); // end  Project_main





});

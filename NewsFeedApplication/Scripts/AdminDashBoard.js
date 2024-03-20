$(document).ready(function () {
    loadCategories();
    loadAgencies();
});

function insertCategory() {
    var categoryName = $("#categoryName").val();
    $.ajax({
        type: "POST",
        url: "/AdminDashBoard/AddNewsCategory",
        data: JSON.stringify({ categoryName: categoryName}),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.success) {
                alert("Category Added successfully!!");
                $("#categoryName").val('');
            } else {
                alert("Failed to insert!!, Please try again.");
            }
        },
        error: function (error) {
            alert("An error occurred while Adding Category , Please try again.");
        }
    });
}

function insertNewsAgency() {
    var agencyName = $("#agencyName").val();
    var agencyUrl = $("#agencyUrl").val();

    var data = {
        agencyName: agencyName,
        agencyUrl: agencyUrl
    };

    $.ajax({
        type: "POST",
        url: "/AdminDashBoard/AddNewsAgency",
        data: JSON.stringify(data), 
        contentType: "application/json; charset=utf-8", 
        dataType: "json",
        success: function (response) {
            if (response.success) {
                alert("Agency Added successfully!!");
                $("#agencyName").val('');
                $("#agencyUrl").val('');
            } else {
                alert("Failed to add agency. Please try again.");
            }
        },
        error: function (error) {
            alert("An error occurred while adding the agency. Please try again.");
        }
    });
}

function loadCategories() {
    $.ajax({
        type: "GET",
        url: "/DashBoard/GetCategories",
        dataType: "json",
        success: function (data) {
            var categorySelect = $("#categorySelect");
            categorySelect.empty(); 
            categorySelect.append($('<option>', {
                value: "",
                text: "--Select a Category--"
            }));
            $.each(data, function (index, category) {
                categorySelect.append($('<option>', {
                    value: category.CategoryId, 
                    text: category.CategoryTitle 
                }));
            });
        },
        error: function (error) {
            alert("Error fetching categories: " + error.responseText);
        }
    });
}

function loadAgencies() {
    $.ajax({
        type: "GET",
        url: "/DashBoard/GetAgencies",
        dataType: "json",
        success: function (data) {
            var agencySelect = $("#agencySelect");
            agencySelect.empty;
            agencySelect.append($('<option>', {
                value: "",
                text: "--Select a News Agency--"
            }));
            $.each(data, function (index, agency) {
                agencySelect.append($('<option>', {
                    value: agency.AgencyId, 
                    text: agency.AgencyName 
                }));
            });
        },
        error: function (error) {
            alert("Error fetching agencies: " + error.responseText);
        }
    });
}

function insertRssFeedForCategoryAndAgency() {
    var selectedAgency = $("#agencySelect").val();
    var selectedCategory = $("#categorySelect").val();
    var rssFeedUrl = $("#rssFeedUrl").val();

    var data = {
        agencyId: selectedAgency,
        categoryId: selectedCategory,
        rssFeedUrl: rssFeedUrl
    };

    $.ajax({
        type: "POST",
        url: "/AdminDashBoard/AddRssFeedForNewsCategory", 
        data: JSON.stringify(data), 
        contentType: "application/json; charset=utf-8", 
        dataType: "json",
        success: function (response) {
            if (response.success) {
                alert("RSS Feed configured successfully!");
                $("#agencySelect").val('');
                $("#categorySelect").val('');
                $("#rssFeedUrl").val('');
            } else {
                alert("Failed to configure RSS Feed. Please try again.");
            }
        },
        error: function (error) {
            alert("An error occurred while configuring the RSS Feed. Please try again.");
        }
    });
}

function confirmDeleteAllNews() {
    if (confirm("Are you sure you want to delete all news?")) {
        deleteAllNews();
    }
}

function deleteAllNews() {
    $.ajax({
        type: "POST",
        url: "/AdminDashBoard/DeleteAllNewsFromTable",
        dataType: "json",
        success: function (response) {
            if (response.success) {
                alert("News table truncated successfully!");
            } else {
                alert("Failed to truncate news table. Please try again.");
            }
        },
        error: function (error) {
            alert("An error occurred while truncating the news table. Please try again.");
        }
    });
}

function generateNewsClickCountReport() {
    var selectedDateFrom = document.getElementById('dateInputFrom').value;
    var selectedDateTo = document.getElementById('dateInputTo').value;

    var formattedDateFrom = new Date(selectedDateFrom).toISOString().split('T')[0];
    var formattedDateTo = new Date(selectedDateTo).toISOString().split('T')[0];

    var data = {
        selectedDateFrom: formattedDateFrom,
        selectedDateTo: formattedDateTo
    };

    $.ajax({
        type: "POST",
        url: "/AdminDashBoard/GenerateNewsClickCountReport",
        data: JSON.stringify(data), 
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (html) {
            $('html').html(html);
            alert("Success: Data displayed on the screen");
        },
        error: function (x, error) {
            alert("Error: Failed to retrieve and display data");
        }
    });
}


function exportToPDF() {
    var doc = new jsPDF();
    var table = document.getElementById("clickCountReport");
    doc.autoTable({ html: table });
    doc.save("News_ClickCountReport.pdf");
}
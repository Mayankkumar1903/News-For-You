$(document).ready(function () {
    loadAgencies();
    loadCategories();

    var agencyId = getCookie('selectedAgency');
    if (agencyId) {
        loadAllNews(agencyId, function () {
            var selectedCategoryIds = JSON.parse(getCookie('selectedCategories') || '[]');
            if (selectedCategoryIds.length > 0) {
                loadNewsBasedOnSelection(selectedCategoryIds, agencyId);
            }
        });
    }
    else {
        alert('Please select an agency to proceed...');
    }

    $('#categoriesContainer').on('change', 'input[name="category"]', function () {
        var selectedCategoryIds = getSelectedCategoryIds();
        var agencyId = getSelectedAgencyId();

        setCookie('selectedCategories', JSON.stringify(selectedCategoryIds), 7);

        if (selectedCategoryIds.length > 0) {
            loadNewsBasedOnSelection(selectedCategoryIds, agencyId);
        } else {
            loadAllNews(agencyId);
        }
    });

    $('#agenciesContainer').on('change', 'input[name="agency"]', function () {
        var agencyId = $(this).val();
        clearCategorySelection();
        setCookie('selectedAgency', agencyId, 7);
        loadAllNews(agencyId);
    });

    setTimeout(function () {
        location.reload();
    }, 300000);
});

function loadCategories() {
    $.ajax({
        type: "GET",
        url: "/DashBoard/GetCategories",
        dataType: "json",
        success: function (data) {
            var categoriesContainer = $("#categoriesContainer");
            categoriesContainer.empty();
            categoriesContainer.append('<h2>Categories</h2>');
            $.each(data, function (index, category) {
                var checkbox = $('<input type="checkbox" id="category-' + category.CategoryId + '" name="category" value="' + category.CategoryId + '" />');
                var label = $('<label for="category-' + category.CategoryId + '" class="custom-checkbox-button">' + category.CategoryTitle + '</label>');
                categoriesContainer.append(checkbox).append(label);
            });

            var selectedCategories = JSON.parse(getCookie('selectedCategories') || '[]');
            var selectedAgency = getCookie('selectedAgency');
            if (selectedCategories.length > 0) {
                $('input[name="category"]').each(function () {
                    if (selectedCategories.includes($(this).val())) {
                        $(this).prop('checked', true);
                    }
                });
            }
        },
        error: function (error) {
            console.log("Error fetching categories:", error);
        }
    });
}

function loadAgencies() {
    $.ajax({
        type: "GET",
        url: "/DashBoard/GetAgencies",
        dataType: "json",
        success: function (data) {
            var agenciesContainer = $("#agenciesContainer");
            agenciesContainer.empty();
            agenciesContainer.append('<h2>Agencies</h2>');

            $.each(data, function (index, agency) {
                var radioBtn = $('<input type="radio" id="agency-' + agency.AgencyId + '" name="agency" value="' + agency.AgencyId + '" />');
                var label = $('<label for="agency-' + agency.AgencyId + '" class="custom-checkbox-button">' + agency.AgencyName + '</label>');
                agenciesContainer.append(radioBtn).append(label);

                var selectedAgency = getCookie('selectedAgency');
                if (selectedAgency) {
                    $('input[name="agency"][value="' + selectedAgency + '"]').prop('checked', true);
                }
            });
        },
        error: function (error) {
            console.log("Error fetching agencies:", error);
        }
    });
}

function getSelectedCategoryIds() {
    var selectedCategoryIds = [];
    $('input[name="category"]:checked').each(function () {
        selectedCategoryIds.push($(this).val());
    });
    return selectedCategoryIds;
}

function clearCategorySelection() {
    $('input[name="category"]').prop('checked', false);
}

function getSelectedAgencyId() {
    return $('input[name="agency"]:checked').val();
}

function loadAllNews(agencyId, callback) {
    $.ajax({
        type: "POST",
        url: "/DashBoard/GetNewsByAgency",
        data: { agencyId: agencyId },
        dataType: "json",
        success: function (data) {
            displayNews(data);
            if (callback) callback();
        },
        error: function (error) {
            console.log("Error fetching news:", error);
        }
    });
}

function loadNewsBasedOnSelection(categoryIds, agencyId) {
    $.ajax({
        type: "POST",
        url: "/DashBoard/GetNewsByCategoryAndAgency",
        data: { categoryIds: categoryIds, agencyId: agencyId },
        dataType: "json",
        success: function (data) {
            displayNews(data);
        },
        error: function (error) {
            console.log("Error fetching news:", error);
        }
    });
}

function displayNews(data) {
    var newsFeedContainer = $("#newsFeedContainer");
    newsFeedContainer.empty();
    $.each(data, function (index, newsItem) {
        var newsItemDiv = $('<div class="news-item"></div>');
        newsItemDiv.append('<div class="news-title">' + newsItem.Title + '</div>');
        newsItemDiv.append('<div class="news-description">' + newsItem.Description + '</div>');
        newsItemDiv.append('<div class="news-date">Published on: ' + newsItem.PubDate + '</div>');
        newsItemDiv.append('<a href="' + newsItem.Link + '" class="read-more">Read More</a>');
        newsFeedContainer.append(newsItemDiv);

        newsItemDiv.find('.read-more').on('click', function (e) {
            console.log(newsItem.NewsFeedId);
            incrementClickCount(newsItem.NewsFeedId);
        });
    });
}

function incrementClickCount(newsId) {
    $.ajax({
        type: "POST",
        url: "/DashBoard/IncrementClickCount",
        data: JSON.stringify({ newsId: newsId }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.success) {
                console.log("Click count incremented successfully.");
            } else {
                console.log("Failed to increment click count.");
            }
        },
        error: function (error) {
            console.log("Error incrementing click count:", error);
        }
    });
}

function setCookie(name, value, days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value || "") + expires + "; path=/";
}

function getCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

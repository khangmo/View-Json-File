/* index will be saved the position each item.  */
var index;

/* Set default value for margin-top */
var marginTopDefault = 10;

/* saving images array follow row */
var arrImages;

/* saving all contents JSON file */
var jsonFile;

var genContents = function (jsonObject, id) {
    var element = '';
    if (jsonObject !== null && jsonObject !== undefined) {
        for (var i = 0; i < jsonObject.length; i++) {
            var li;
            if (i === 0 && i === (jsonObject.length - 1)) {
                li = '<li data-role="collapsible" data-iconpos="right" data-inset="false" '
                        + 'style="border-width: 1px 0 0; border-style: solid; list-style: none;" '
                        + 'class="ui-collapsible ui-collapsible-themed-content ui-li-static ui-body-inherit ui-first-child ui-last-child">';
            } else {
                if (i === 0 && i < (jsonObject.length - 1)) {
                    li = '<li data-role="collapsible" data-iconpos="right" data-inset="false" '
                            + 'style="border-width: 1px 0 0; border-style: solid;  list-style: none;" '
                            + 'class="ui-collapsible ui-collapsible-themed-content ui-li-static ui-body-inherit ui-first-child">';
                } else if (i > 0 && i < (jsonObject.length - 1)) {
                    li = '<li data-role="collapsible" data-iconpos="right" data-inset="false" '
                            + 'style="border-width: 1px 0 0; border-style: solid;  list-style: none;"'
                            + 'class="ui-collapsible ui-collapsible-themed-content ui-collapsible-collapsed ui-li-static ui-body-inherit">'
                } else {
                    li = '<li data-role="collapsible" data-iconpos="right" data-inset="false" '
                            + 'style="border-width: 1px 0 0; border-style: solid;  list-style: none;"'
                            + 'class="ui-collapsible ui-collapsible-themed-content ui-collapsible-collapsed ui-li-static ui-body-inherit ui-last-child">';
                }
            }

            element += li + '<h5 class="ui-collapsible-heading ui-collapsible-heading-collapsed"><a href="#" class="ui-collapsible-heading-toggle ui-btn ui-btn-inherit">' + jsonObject[i].timeString + '</a></h5>'
                    + '<fieldset data-role="controlgroup" data-type="horizontal">'
                    + '<table data-role="table" id="phone-table" data-mode="columntoggle" data-column-btn-theme="a" class="phone-compare ui-shadow table-stroke">'
                    + '<tbody><tr class="photos"><th class="label">Images</th>';
            var images = jsonObject[i].images;
            if (images !== null && images !== undefined) {
                for (var j = 0; j < images.length; j++) {
                    element += '<td><a class="select-index-image" href="#myPopupDialog" index="' + i + '-' + j + '" data-rel="popup" data-position-to="window"><img src="'
                            + images[j].thumbUrl + '""></a></td>';
                }
            }
            element += '</tr></tbody></table></fieldset></li>';
        }
    }
    $(id).empty().append(element);
    $(id).fadeIn("slow");
};

var showImageByJson = function (json, id) {
    $(id).empty();
    try {
        jsonFile = json;

        /* Show thumbnail image */
        genContents(jsonFile, id);
    } catch (err) {
        $(object).append('<div style="color:red; font-weight: bold">JSON ERROR! ' + err.toString() + '</div>');
    }
    /* Show JSON content */
    $(id).fadeIn("slow");

    /* Set event when Users select one thumbnail image */
    $(".select-index-image").click(function () {
        $("#thumbnailImage").fadeOut();

        var tokenIndexs = $(this).attr("index").split("-");
        console.log(tokenIndexs);
        if (tokenIndexs.length > 1) {
            $("#timeLabelDialog").empty().append(jsonFile[tokenIndexs[0]].time);
            arrImages = jsonFile[tokenIndexs[0]].images;
            console.log(arrImages);
            console.log(arrImages[tokenIndexs[1]].imageUrl);
            $("#thumbnailImage").attr("src", arrImages[tokenIndexs[1]].imageUrl);
            $("#thumbnailImage").fadeIn("slow");
            index = tokenIndexs[1];
        } else {
            alert("Error Ocurred!");
        }
    });
};

//
var setHeaderMargin = function (id) {
    var heightBar = $(".navbar.navbar-inverse.navigation-bar.navbar-fixed-top").height();
    $(id).css({ "margin-top": heightBar + marginTopDefault });
};

$(document).ready(function () {
    /* Auto set margin-top when resize browser */

    /* Home/Index */
    $(window).resize(function () {
        setHeaderMargin("#form-input-account");
        setHeaderMargin("#configuration-quartz-scheduler");
    });

    /* Call this function when Users select one JSON file */
    $("input:file").change(function (evt) {
        if (!browserSupportFileUpload()) {
            return "The File APIs are not fully supported in this browser!";
        } else {
            var file = evt.target.files[0];
            var reader = new FileReader();
            reader.readAsText(file);
            reader.onload = function (event) {
                $("#showContentJson").empty();
                try {
                    var jsonObject = jQuery.parseJSON(event.target.result);
                    jsonFile = jsonObject;
                    /* Show thumbnail image */
                    genContents(jsonObject);

                    /* Show JSON contents */
                    document.getElementById("showContentJson").appendChild(document.createElement("pre"))
                            .innerHTML = syntaxHighlightJSON(JSON.stringify(jsonObject, undefined, 4));
                } catch (err) {
                    $("#showContentJson").append('<div style="color:red; font-weight: bold">JSON ERROR! ' + err.toString() + '</div>');
                }
                /* Show JSON content */
                $("#showContentJson").fadeIn("slow");
                
                /* Set event when Users select one thumbnail image */
                $(".select-index-image").click(function () {
                    $("#thumbnailImage").fadeOut();
                    
                    var tokenIndexs = $(this).attr("index").split("-");
                    console.log(tokenIndexs);
                    if (tokenIndexs.length > 1) {
                        $("#timeLabelDialog").empty().append(jsonFile[tokenIndexs[0]].time);
                        arrImages = jsonFile[tokenIndexs[0]].images;
                        console.log(arrImages);
                        console.log(arrImages[tokenIndexs[1]].imageUrl);
                        $("#thumbnailImage").attr("src", arrImages[tokenIndexs[1]].imageUrl);
                        $("#thumbnailImage").fadeIn("slow");
                        index = tokenIndexs[1];
                    } else {
                        alert("Error Ocurred!");
                    }
                });
            };
        }
    });

    /* When Users click to Forward button */
    $("#moveForward").click(function () {
        index--;
        if (index !== null && index !== undefined) {
            if (index < 0) {
                index = arrImages.length - 1;
            }
            $("#thumbnailImage").fadeOut();
            $("#thumbnailImage").attr("src", arrImages[index].imageUrl);
            $("#thumbnailImage").fadeIn();
        }
    });

    /* When Users click to Next button */
    $("#moveNext").click(function () {
        index++;
        if (index !== null && index !== undefined) {
            if (index >= arrImages.length) {
                index = 0;
            }
            $("#thumbnailImage").fadeOut();
            $("#thumbnailImage").attr("src", arrImages[index].imageUrl);
            $("#thumbnailImage").fadeIn();
        }
    });
});

/**
 * 
 * @param {type} evt
 * @returns {String}
 */
var detectFile = function (evt) {
    if (!browserSupportFileUpload()) {
        return "The File APIs are not fully supported in this browser!";
    } else {
        var file = evt.target.files[0];
        var reader = new FileReader();
        reader.readAsText(file);
        reader.onload = function (event) {
            j = jQuery.parseJSON(event.target.result);
            return j;
        };
    }
};

/**
 * browserSupportFileUpload check browser support file upload
 * 
 * @returns {Boolean}
 */
var browserSupportFileUpload = function () {
    var isCompatible = false;
    if (window.File && window.FileReader && window.FileList && window.Blob) {
        isCompatible = true;
    }
    return isCompatible;
};

/** 
 * syntaxHighlightJSON add tag element for JSON String
 * 
 * @param {String} json
 * @returns {String}
 */
var syntaxHighlightJSON = function (json) {
    json = json.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    return json.replace(/("(\\u[a-zA-Z0-9]{4}|\\[^u]|[^\\"])*"(\s*:)?|\b(true|false|null)\b|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?)/g, function (match) {
        var cls = "number_json";
        if (/^"/.test(match)) {
            if (/:$/.test(match)) {
                cls = "key_json";
            } else {
                cls = "string_json";
            }
        } else if (/true|false/.test(match)) {
            cls = "boolean_json";
        } else if (/null/.test(match)) {
            cls = "null_json";
        }
        return '<span class="' + cls + '">' + match + '</span>';
    });
};

/**
 * formatJSON setting JSON format
 * 
 * @param {String} json
 * @returns {String}
 */
var formatJSON = function (json) {
    if (json !== null && json !== undefined) {
        try {
            return JSON.stringify(JSON.parse(json), undefined, 4);
        } catch (err) {
            return "JSON Error";
        }
    } else {
        return "";
    }
};

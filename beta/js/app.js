$(document).ready(function() {
    var flagPoint = false;
    var flagMenu = false;
    var allOpen = true;
    var videoWindow = null;
    var devMenu = [];
    var mapPoints = [];
    var actMap = [];
    var nvrList = [];
    var cameraList = [];

    // init
    $("#siteMenu").hide();

    // load json files
    loadJsonFile("data/account.json", getAccountInfo);
    loadJsonFile("data/menu.json", getMenuInfo);
    loadJsonFile("data/map.json", getMapInfo);
    loadJsonFile("data/dev.json", getDevInfo);
    loadJsonFile("data/act.json", getActInfo);

    // event binding
    $("#loginBtn").bind("click", function() {
        $(".errMsg").empty();
        var inputAccount = $("#inputAccount").val();
        var inputPassword = $("#inputPassword").val();

        if (inputAccount === "") {
            $(".errMsg").append("帳號不得為空");
            return;
        }
        if (inputPassword === "") {
            $(".errMsg").append("密碼不得為空");
            return;
        }
		var check = false;
		for (var i = 0; i < account.length; i++) {
			if (inputAccount === account[i].name) {
				if (inputPassword === account[i].pass) {	
					check = true;
					break;
				} else {
					$(".errMsg").append("密碼錯誤");
					return;
				}
			}
		}

		if (check === false) {
			$(".errMsg").append("帳號不存在");
			return;
		}
			
        $("#inputPassword").val("");

        $("#loginWnd").hide();
        $("#mainWnd").show();
		getAllInfo();
        //adjustSize();
    });

    $(".logout").bind("click", function() {
        $("#mainWnd").hide();
        $("#loginWnd").show();
    });

    $(".cameraIcon").bind("click", function() {
        alert(this.id);
    });

    $(".domeIcon").bind("click", function() {
        alert(this.id);
    });

    $("#siteMenu").mouseover(function() {
        flagMenu = true;
    });

    $("#siteMenu").mouseleave(function() {
        flagMenu = false;
        $("#siteMenu").hide();
    });

    $(".mask").bind("click", function() {
        $(this).hide();
        $("#siteMenu").hide();
    });

    $(document).keypress(function(e) {
        if (e.which == 13) {
            $("#loginBtn").trigger('click');
        }
    });

    $(window).resize(function() {
        //adjustSize();
    });

    function getAllInfo() {
        getRainInfo(-1, function (html){
            $("#rainInfo").append(html);
        });
        getWaterInfo(-1, function (html){
            $("#waterInfo").append(html);
        });
        getEyeInfo(-1, function (html){
            $("#eyeInfo").append(html);
        });
    }

    function getEyeInfo(id, callback) {
        var phpUrl = "eye/eyefalcon.php?id=" + id;
        $.ajax({
            url: phpUrl,
            complete: function(response) {
                if (response.responseJSON) {
                    var html = getEyeTableHtml(response.responseJSON, "隼眼", "danger");
                    callback(html);
                }
            },
            error: function() {
                alert("Error getEyeInfo!");
            }
        });
    }

    function getWaterInfo(id, callback) {
        var phpUrl = "db/waterlevel.php?id=" + id;
        $.ajax({
            url: phpUrl,
            complete: function(response) {
                if (response.responseJSON) {
                    var html = getTableHtml(response.responseJSON,"水位(單位:m)", "success");
                    callback(html);
                }
            },
            error: function() {
                alert("Error getWaterInfo!");
            }
        });
    }

    function getRainInfo(id, callback) {
        var phpUrl = "db/rainfall.php?id=" + id;
        $.ajax({
            url: phpUrl,
            complete: function(response) {
                if (response.responseJSON) {
                    var html = getTableHtml(response.responseJSON, "雨量(單位:mm)", "info");
                    callback(html);
                }
            },
            error: function() {
                alert("Error getRainInfo!");
            }
        });
    }

    function adjustSize() {
        // var navHeight = $(".serverNav").outerHeight();
        // if (navHeight > 650) {
        //     $(".waterInfo").css("height", navHeight - 500);
        // } else {
        //     $(".waterInfo").css("height", 150);
        // }
    }

    function updateMenu(id) {

    }

    function checkMenu() {
        if (!flagPoint && !flagMenu) {
            $("#siteMenu").hide();
        }
    }

    function handleActionList(actList) {
        var actType;
        for (var i = 0; i < actList.length; i++) {
            handleAction(actList[i].act, actList[i].param, true);
        }
    }

    function handleAction(act, param, allin1) {
        switch (act) {
            case '1':
            case '2':
            case '3':
            case '4':
                var i, j, k;
                var nvr = {};
                var video = [];
                var selectCameraList;
                var html = "";

                nvr["id"] = param[0].nvr;
                for (i = 0; i < nvrList.length; i++) {
                    if (param[0].nvr === nvrList[i]["id"]) {
                        nvr['ip'] = nvrList[i]["ip"];
                        nvr['account'] = nvrList[i]["user"];
                        nvr['password'] = nvrList[i]["password"];
                        break;
                    }
                }

                for (i = 0; i < cameraList.length; i++) {
                    if (cameraList[i].nvr === param[0].nvr) {
                        selectCameraList = cameraList[i];
                        break;
                    }
                }

                for (i = 0; i < param.length; i++) {
                    for (j = 0; j < selectCameraList.list.length; j++) {
                        if (param[i]["camera"] === selectCameraList.list[j]["id"]) {
                            video.push(selectCameraList.list[j]);
                            break;
                        }
                    }
                }

                html = getActivexHtml(nvr, video);
                videoWindow = window.open('video.html', "Video");
                videoWindow.activexContent = html;
                videoWindow.nvr = nvr;
                videoWindow.video = video;
                setTimeout(function(){ 
                    videoWindow.activexContent = html;
                    videoWindow.nvr = nvr;
                    videoWindow.video = video;
                    videoWindow.initActiveX();
                }, 50);
                setTimeout(function(){ videoWindow.connect(); }, 100);

                break;
           case '7':
                // 雨量 rainfall
				if (allin1) {
                    var id = param[0]["id"];
                    getRainInfo(id, function (html){
                        if (videoWindow) {
                            videoWindow.infoTableContent = html;
                            videoWindow.initInfoTable();
                        }
                    });
                } 
				else {
					var info = {};
					info['id'] = param[0].id;
					infoWindow =  window.open('rainfall.html', "info");
					
					setTimeout(function(){ infoWindow.getInfo(info); }, 200);	
				}
				
                break;
            case '8':
                // 水位 waterLevel
				if (allin1) {
                    var id = param[0]["id"];
                    getWaterInfo(id, function (html){
                        if (videoWindow) {
                            videoWindow.infoTableContent = html;
                            videoWindow.initInfoTable();
                        }
                    });
                }
				else {
					var info = {};
					info['id'] = param[0].id;
					infoWindow =  window.open('waterLevel.html', "info");
					
					setTimeout(function(){ infoWindow.getInfo(info); }, 200);	
				}
                break;
            case '10':
                // 新視窗
				var url = param[0].url;
				window.open(url);
                break;
            case '11':
                // 準眼
				 if (allin1) {
                    var id = param[0]["id"];
                    getEyeInfo(id, function (html){
                        if (videoWindow) {
                            videoWindow.infoTableContent = html;
                            videoWindow.initInfoTable();
                        }
                    });
                }
				else {
					var info = {};
					info['id'] = param[0].id;
					infoWindow =  window.open('eye.html', "info");
					
					setTimeout(function(){ infoWindow.getInfo(info); }, 200);	
				}
				
                break;
            case '12':
                // 地震儀 earthquake
				var info = {};
				info['id'] = param[0].id;
				infoWindow =  window.open('earthquake.html', "info");
				
				setTimeout(function(){ infoWindow.getInfo(info); }, 200);
                break;
            case '13':
                // 量水堰 weir
				var info = {};
				info['id'] = param[0].id;
				infoWindow =  window.open('weir.html', "info");
				
				setTimeout(function(){ infoWindow.getInfo(info); }, 200);
                break;
            case '14': 
                // 地下水井 groundWater
				var info = {};
				info['id'] = param[0].id;
				infoWindow =  window.open('groundWater.html', "info");
				
				setTimeout(function(){ infoWindow.getInfo(info); }, 200);
                break;
            default:
                break;
        }
    }

	function getTableHtml(data, name, className) {
        var html = "";
        html += "<table class='table table-striped table-bordered'><tr><td style='font-weight:bold;'>地點</td>";
        for (var i = 0; i < data.length; i++) {
            html += "<td>" + data[i].name + "</td>";
        }
		html += "<td>最後更新</td>";
        html += "</tr>" + "<tbody><tr class='" + className +"'>" + "<td style='font-weight:bold;'>" + name + "</td>";
        for (var j = 0; j < data.length; j++) {
            html += "<td>" + data[j].value + "</td>";
        }
		html += "<td>" + data[0].lastTime + "</td>";
        html += "</tr></tbody></table>";

        return html;
    }
	
    function getEyeTableHtml(data, name, className) {
        var html = "";
        html += "<table class='table table-striped table-bordered'><tr><td style='font-weight:bold;'>地點</td>";
        for (var i = 0; i < data.length; i++) {
            html += "<td>" + data[i].name + "</td>";
        }
		html += "<td>最後更新</td>";
        html += "</tr>" + "<tbody><tr class='" + className +"'>" + "<td style='font-weight:bold;'>" + '市電電壓' + "</td>";
        for (var j = 0; j < data.length; j++) {
            html += "<td>" + data[j].di + "</td>";
        }
		html += "<td>" + data[0].lastTime + "</td>";
        html += "</tr>" + "<tr class='" + className +"'>" + "<td style='font-weight:bold;'>" + 'UPS輸出電壓' + "</td>";
        for (var k = 0; k < data.length; k++) {
            html += "<td>" + data[k].do_out + "</td>";
        }
		html += "<td>" + data[0].lastTime + "</td>";
        html += "</tr></tbody></table>";

        return html;
    }

    function getActivexHtml(nvr, camList) {
        var html = "";
        var posX, posY;
        var videoWidth = getVideoWidth(camList.length);
        var videoHeight = getVideoHeight(camList.length);
        var hasAxis = false;
        for (var i = 0; i < camList.length; i++) {
            posX = getVideoPos(i, camList.length, videoWidth, videoHeight, true);
            posY = getVideoPos(i, camList.length, videoWidth, videoHeight, false);
            html += "<div style='position:absolute;left: " +　posX + "px; top: " + posY + "px;'>";
            if (camList[i]["brand"] === "axis") {
                hasAxis = true;
                html += "<OBJECT ID='MyActiveX" + i + "' WIDTH=" + videoWidth + " HEIGHT=" + videoHeight + " CLASSID='CLSID:745395C8-D0E1-4227-8586-624CA9A10A8D' " +
                        "codebase='AMC.cab'>" + "<param name='AutoStart' value='0'>" + "<param name='UIMode' value='ptz-absolute'>" +
                        "<param name='MediaType' value='h264'>" + "<param name='VideoRenderer' value='65536'>" +
                        "<param name='NetworkTimeout' value='5000'>" + "</OBJECT>";
            } else if (camList[i]["brand"] === "samsung"){
                html += "<OBJECT ID='MyActiveX" + i + "' WIDTH=" + videoWidth + " HEIGHT=" + videoHeight + " CLASSID='CLSID:36299202-09EF-4ABF-ADB9-47C599DBE778' " +
                        "codebase='myactivex.cab#version=1,0,0,1'></OBJECT>";
            }

            html += "</div>";
        }
        if (hasAxis) {
            html += "<OBJECT ID='h264dec' classid='CLSID:7340F0E4-AEDA-47C6-8971-9DB314030BD7', codebase='h264_dec.cab'></OBJECT>";
        }

        return html;
    }

    function getVideoWidth(count) {
        var type = getType(count);
        var width;
        switch(type) {
            case 4:
                // 4x4
                width = 240;
                break;
            case 3:
                // 3x3
                width = 360;
                break;
            default:
                width = 480;
                break;
        }
        return width;
    }

    function getVideoHeight(count) {
        var type = getType(count);
        var height;
        switch(type) {
            case 4:
                // 4x4
                height = 160;
                break;
            case 3:
                // 3x3
                height = 240;
                break;
            default:
                height = 320;
                break;
        }
        return height;
    }

    function getType(count) {
        if (count > 9) {
            return 4;
        } else if (count > 4) {
            return 3;
        } else if (count > 1) {
            return 2;
        } else {
            return 1;
        }
    }

    function getVideoPos(idx, total, w, h, x) {
        var pos = 10;
        var unitPx = (x)? w: h;
        var max = 1;
        var unit = 0;
        if (total > 9) {
            max = 4;
        } else if (total > 4) {
            max = 3;
        } else if (total > 1) {
            max = 2;
        }

        unit = (x)? (idx % max): (Math.floor(idx/max));

        pos += unit * (unitPx + 10);
        return pos;
    }

    function initMenu() {
    	var htmlContent = "";
        var devIDSelect = "";
        $(".serverNav").empty();
        // layer 1
        htmlContent += "<div><img src='image/folder.png' class='folderIcon'/></div>";

        // layer 2
        htmlContent += "<ul>";
    	for (var i = 0; i < devMenu.length; i++) {
            htmlContent += "<li><div><img src='image/nvr.png' class='nvrIcon'/>" +
                           "<span>" + devMenu[i].name + "</span>";

            // layer 3
            htmlContent += "<ul>";
            for (var j = 0; j < devMenu[i].content.length; j++) {

                if (devMenu[i].content[j].icon === '2') {
                    htmlContent += "<li><img src='image/dvr.png' class='dvrIcon'/>";
                }
                else if (devMenu[i].content[j].icon === '3') {
                    htmlContent += "<li><img src='image/camera.png' class='cameraIcon'/>";
                } 
                else if (devMenu[i].content[j].icon === '5') {
                    htmlContent += "<li><img src='image/dome.png' class='domeIcon'/>";
                }
                else {
                    htmlContent += "<li><img src='image/utility.png' class='utilityIcon'/>";
                }

                if (devMenu[i].content[j].multi === '1') {
                    htmlContent += "<span>" + devMenu[i].content[j].name + "</span>";
                    // layer 4
                    htmlContent += "<ul>";
                    for (var k = 0; k < devMenu[i].content[j].content.length; k++) {
                        if (devMenu[i].content[j].content[k].icon === '2') {
                            htmlContent += "<li><img src='image/dvr.png' class='dvrIcon'/>";
                        }
                        else if (devMenu[i].content[j].content[k].icon === '3') {
                            htmlContent += "<li><img src='image/camera.png' class='cameraIcon'/>";
                        }
                        else if (devMenu[i].content[j].content[k].icon === '5') {
                            htmlContent += "<li><img src='image/dome.png' class='domeIcon'/>";
                        } 
                        else {
                            htmlContent += "<li><img src='image/utility.png' class='utilityIcon'/>";
                        }
                        htmlContent += "<span class='devAction'id='dev-" + i + "-" + j + "-" + k + "'>" + devMenu[i].content[j].content[k].name + "</span></li>"
                        devIDSelect = "#dev-" + i + "-" + j + "-" + k;
                    }
                    htmlContent += "</ul>";
                } else {
                    htmlContent += "<span class='devAction' id='dev-" + i + "-" + j + "'>" + devMenu[i].content[j].name + "</span>";
                    devIDSelect = "#dev-" + i + "-" + j;
                }
                htmlContent += "</li>";
            }
            htmlContent += "</div></li>";
    	}
        htmlContent += "</ul>";

        $(".serverNav").append(htmlContent);

        // bind animation
        $(".folderIcon").bind("click", function(){
            for (var s = 0; s　< $(".nvrIcon").length; s++) {
                if (allOpen) {
                    $($(".nvrIcon")[s]).siblings("ul").slideUp();
                } else {
                    $($(".nvrIcon")[s]).siblings("ul").slideDown();
                }
            }
            allOpen = !allOpen;
        });
        $(".nvrIcon").bind("click", function(){
            $(this).siblings("ul").slideToggle();
        });
        $(".utilityIcon").bind("click", function(){
            $(this).siblings("ul").slideToggle();
        });

        // bind action
        $(".devAction").bind("click", function(){
            var id = this.id;
            if (id) {
                var tmp = id.split("-");
                var i, j, k;
                i = parseInt(tmp[1], 10);
                j = parseInt(tmp[2], 10);
                if (tmp.length === 4) {
                    k = parseInt(tmp[3], 10);
                    handleAction(devMenu[i].content[j].content[k].act, devMenu[i].content[j].content[k].param);
                } else if (tmp.length === 3) {
                    handleAction(devMenu[i].content[j].act, devMenu[i].content[j].param);
                }
            }
        });
    }

    function initPoints() {
        var htmlContent = "";
        $(".mapPoints").empty();
        for (var i = 0; i < mapPoints.length; i++) {
            htmlContent = "<img src='image/point.png' class='point site' style='left:" +
                mapPoints[i].pos.x + "px; top:" +
                mapPoints[i].pos.y + "px'" +
                "id='" + mapPoints[i].id + "'/>" +
                "<div class='siteTitle' style='left:" +
                mapPoints[i].menu_pos.x + "px; top:" +
                mapPoints[i].menu_pos.y + "px'>" +
                "<span>" + mapPoints[i].name + "</span></div>";

            $("#mapPoints").append(htmlContent);
        }

        $(".point").mouseleave(function() {
            flagPoint = false;
            setTimeout(checkMenu, 500);
        });

        $(".site").mouseover(function() {
            flagPoint = true;
            var id = this.id;

            var index = -1;
            for (var i = 0; i < mapPoints.length; i++) {
                if (mapPoints[i].id === id) {
                    index = i;
                    break;
                }
            }

            if (index < 0) {
                return;
            }

            $("#siteMenu").empty();

            var menuID;
            var menuIDSelect;
            $("#siteMenu").append(mapPoints[index].name+ "<br>");
            for (var j = 0; j < mapPoints[index].content.length; j++) {
                menuID = "menu" + j;
                $("#siteMenu").append("<span id='" + menuID + "'>" + mapPoints[index].content[j].name + "</span><br>");
                menuIDSelect = "#" + menuID;

                $(menuIDSelect).bind("click", { act: mapPoints[index].content[j].actList }, function(event) {
                    handleActionList(event.data.act);
                });
            }

            var left = parseInt(mapPoints[index].menu_pos.x, 10);
            var top = parseInt(mapPoints[index].menu_pos.y, 10);

            switch(id) {
            	case 'point4':
            		left = 434;
            		break;
            	case 'point6':
            		left = 459;
            		break;
            	case 'point8':
            		left = 40;
            		break;
            	case 'point9':
            		left = 752;
            		break;
            	default:
            		break;
            }

            $("#siteMenu").css("left", left); 
            $("#siteMenu").css("top", top);

            $("#siteMenu").show();
            $(".mask").show();
        });
    }

    function getMenuInfo(data) {
    	if (data) {
            devMenu = data.menu;
            initMenu();
        }
    }

    function getMapInfo(data) {
        if (data) {
            mapPoints = data.point;
            initPoints();
        }
    }

    function getActInfo(data) {
    	if (data) {
    		actMap = data.act;
    	}
    }

	function getAccountInfo(data) {
    	if (data) {
    		account = data.account;
    	}
    }

	    function getDevInfo(data) {
        if (data) {
            nvrList = data.nvr;
            cameraList = data.camera;
        }
    }

    function loadJsonFile(url, callback) {
        $.ajax({
            url: url,
            type: "GET",
            data: "",
            success: function(data) {
                callback(data);
            }
        })
    }

    function padLeft(str) {
        if (str.length > 1) {
            return str;
        } else {
            return "0" + str;
        }
    }
});

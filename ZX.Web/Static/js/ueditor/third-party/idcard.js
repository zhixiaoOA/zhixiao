// JavaScript Document

//************************************
//	身份证认证JS  BY　ＨＩＴＯＮ
//
//	checkChineseID(输入身份证的文本框ID)
//***********************************


// 提示框
function idcardalert(msg){
	window.alert(msg);
}

//验证身份证长度
function checkIdLen(code){
	if(code==null || ( code.length!=15 && code.length!=18)){
		return false;
	} else {
		return true;	
	}
}

//验证身份证字符合法性
function checkStrValue(code){
	for(var i=0;i<code.length;i++){
		if(i<17){ //判断其他位是否数字
 			if(code.charAt(i)>"9" || code.charAt(i)<"0"){
 			return false;
 			}
		}
		if(i == 17){ //判断17位是否合法
			if(!((code.charAt(i) >= "0" && code.charAt(i) <= "9" )|| code.charAt(i)=='X' || code.charAt(i)=='x')){ 
				return false;
			}
		}
	}
	return true;
	
}

//验证并获取所在地区
function getCityName(citycode){
	//县的名字
	var cityname = cities[citycode];
	if(cityname == null){
		return null; //县城名字没有 返回空值	
	}
	
	//加上所在市的名字
	if(citycode%100 != 0 && (citycode-citycode%100)!=659000 && (citycode-citycode%100)!=429000 && (citycode-citycode%100)!=469000){
		var c_name =	cities[(citycode-citycode%100)];
		cityname=c_name+cityname;
	}
	
	//加上所在省的名字
	if(citycode%10000 != 0){
		var p_name = cities[(citycode-citycode%10000)];
		cityname=p_name+cityname;
	}
	
	return cityname; //返回城市名字
}

//验证兵获取出生年月信息
function getBirthday(year,month,day){
		if(year<=1900){
			idcardalert('出生年错误');
			return false;
		}
		if(month<=0 || month > 12){
			idcardalert('出生月错误');
			return false;
		}
		if(day<=0 || day > 31){
			idcardalert('出生日错误');
			return false;
		}
		
		if(year%4 == 0){
			if(month == 2){
				if(day > 29){
					idcardalert('出生日错误');
					return false;
				}
			}
		}
		
		if(year%4 != 0){
			if(month == 2){
				if(day > 28){
					idcardalert('出生日错误');
					return false;
				}
			}
		}
		
		var word = year+'年'+month+'月'+day+'日';
		return word;
}

//验证兵获取性别
function getSex(sexnum){
	var sex = '';
	 if(sexnum%2==1){
 		sex="男";
 	}else{ 
 		sex="女";
	}
	return sex;
}

//15 18位身份证互换
function get1518(code,which){
	var code15 = "";
	var code18 = "";
	if(code.length == 18){
		code18=code;
		code15=code.substr(0,6)+code.substr(8,9);
			if(which == 15){
				return code15;
			}
			if(which = 18){
				return code18;
			}
	}
	
	if(code.length == 15){
		code15=code;
		code18=code.substr(0,6)+'19'+code.substr(6,9);
			if(which == 15){
				return code15;
			}
			if(which == 17){
				return code18;
			}
			if(which == 18){
				return code18+getLastP(code);
			}
	}
}

//获得尾数验证数
function getLastP(code){ 
	var end 	= new Array('1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2'); //尾数数组
	var c2 		= new Array('7', '9', '10', '5', '8', '4', '2', '1', '6', '3', '7', '9', '10', '5', '8', '4', '2'); //因子数组
	var temp_end	= 0;
	
	//获取前17位 如果是15位身份证 补全17位数
	if(code.length == 15){
		code = get1518(code,'17');
	}
	code = code.substr(0,17);
	
	for(var i=0;i<code.length;i++){
		temp_end = temp_end + parseInt(code.substr(i,1)) * c2[i];
	}
	
	var last = temp_end%11; 
	var last = end[last];
	return last;
}

function checkName(name){
	var name = document.getElementById(name).value;
	if(name == ""){
		return false;
	} else {
		return true;
	}
}
//*********************************************************************************
// 认证过程
//*********************************************************************************

function checkChineseID(target,nameid,formid){
	var code = document.getElementById(target).value;
	
	//检查长度
	if(!checkIdLen(code)){
		idcardalert('身份证长度错误');
		return false;
	}
	
	//检查是否都是合法数字和字母
	if(!checkStrValue(code)){
		idcardalert('身份证格式错误');
		return false;
	}
	
	//地区码确认
	var citycode = code.substr(0,6);
	var s_cityname = getCityName(citycode);

	if(s_cityname == null){
		idcardalert('地区码不存在！');
		return false;
	}
	
	//出生日期确认
	var year 	= 1900;
	var month	= 1;
	var day		= 1;
	var s_birthday;
	if(code.length == 15){
		year = 1900+parseInt(code.substr(6,1))*10+parseInt(code.substr(7,1));
		month= parseInt(code.substr(8,1))*10+parseInt(code.substr(9,1));
		day	 = parseInt(code.substr(10,1))*10+parseInt(code.substr(11,1));
			if(!getBirthday(year,month,day)){
				return false;
			} else {
				s_birthday = getBirthday(year,month,day);
			}		
	}
	
	if(code.length == 18){
		year=parseInt(code.substr(6,1))*1000+parseInt(code.substr(7,1))*100+parseInt(code.substr(8,1))*10+parseInt(code.substr(9,1));
		month= parseInt(code.substr(10,1))*10+parseInt(code.substr(11,1));
		day	 = parseInt(code.substr(12,1))*10+parseInt(code.substr(13,1));
			if(!getBirthday(year,month,day)){
				return false;
			} else {
				s_birthday = getBirthday(year,month,day);
			}
	}
	
	//年龄
	var s_age = "";
	s_age=parseInt(new Date().getFullYear())-parseInt(year);
	
	//性别判断
	var s_sex;
	var sexnum;
	if(code.length == 15){
		sexnum=parseInt(code.substr(14,1));
		s_sex = getSex(sexnum);
	}
	
	if(code.length == 18){
		sexnum=parseInt(code.substr(16,1));
		s_sex = getSex(sexnum);
	}
	
	//18位身份证验证尾数确认
	var checklast = getLastP(code);
	
	if(code.length == 18){
		lastnumber	= code.substr(17,1);
		lastnumber	= lastnumber.toUpperCase();
		if(checklast != lastnumber){
			idcardalert('身份证号码错误！');
			return false;
		}
	}
	
	if(nameid!='' && !checkName(nameid)){
		idcardalert('姓名不能为空！');
		return false;
	}
	
	//输出信息
	if(nameid!='')
	{
	var name = document.getElementById(nameid).value;
	
	var s_msg = "您的姓名:\t"+name+"\n\n"
	+ "您输入的身份证号码:\t"+code+"\n\n"
 	+ "对应15位号码为：\t"+get1518(code,'15')+"\n\n"
	+ "对应18位号码为：\t"+get1518(code,'18')+"\n\n"
 	+ "所在地区：\t"+s_cityname+"\n\n"
 	+ "出生日期：\t"+s_birthday+" \n\n"
	+ "年龄：\t"+s_age+" 岁\n\n"
	+ "性别：\t"+s_sex+"\n";
	
	
	mymes=confirm(s_msg);
	if(mymes==true){	
	   if(formid!='')
	   {	
		document.getElementById(formid).submit();
	    }
	    else
	    {
	        return true;
	    }
	} else {
		return false;
	}
	}
	else
	{
	    return true;
	}
	//window.alert(s_msg);
	
	
}
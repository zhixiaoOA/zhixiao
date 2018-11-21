// JavaScript Document
/*!
 * lhgcore Dialog Plugin Demo
 * Copyright (c) 2009 - 2011 By Li Hui Gang
 * URL : http://lhgcore.com/
 * BLOG : http://t.qq.com/lhgcore/
 */

// ���д���
$.fn.runCode = function () {
	var getText = function(elems) {
		var ret = "", elem;

		for ( var i = 0; elems[i]; i++ ) {
			elem = elems[i];
			if ( elem.nodeType === 3 || elem.nodeType === 4 ) {
				ret += elem.nodeValue;
			} else if ( elem.nodeType !== 8 ) {
				ret += getText( elem.childNodes );
			};
		};

		return ret;
	};
	
	var code = getText(this);
	new Function(code).call(window);
	
	return this;
};

// Ƥ��ѡ��	
window._demoSkin = function () {
	$.dialog({
		id: 'demoSkin',
		padding: '15px',
		title: 'lhgDialogƤ��չʾ',
		content: _demoSkin.tmpl
	});
};
_demoSkin.tmpl = function (data) {
	var html = ['<table class="zebra" style="width:480px"><tbody>'];
	for (var i = 0, length = data.length; i < length; i ++) {
		html.push('<tr class="');
		html.push(i%2 ? 'odd' : '');
		html.push('"><th style="width:7em"><a href="?demoSkin=');
		html.push(data[i].name);
		html.push('">');
		html.push(data[i].name);
		html.push('</a></th><td>');
		html.push(data[i].about);
		html.push('</td></tr>');
	};
	html.push('</tbody></table>');
	return html.join('');
}([
	{name: 'default', about: '��ɫ��Լ���Ƥ��������ʮ�㣬��CSS��ƣ���ͼƬ������css3������ǿ'},
	{name: 'iblue', about: '�����ɫ��͸��Ƥ���������������һ�£�����ͼƬ'},
	{name: 'igreen', about: '�����ɫ��͸��Ƥ���������������һ�£�����ͼƬ'},
	{name: 'iblack', about: '����ɫ��͸��Ƥ���������������һ�£�����ͼƬ'},
	{name: 'blue', about: '3.5.2Ĭ����ɫ͸��Ƥ���������������һ�£�����ͼƬ'},
	{name: 'chrome', about: 'chrome�����(xp)��񣬰���ͼƬ'},
	{name: 'discuz', about: 'discuz��̳��¼������ʽƤ����࣬��CSS��ƣ���ͼƬ������css3������ǿ'},
	{name: 'jtop', about: '��cmstop���ݹ���ϵͳ����Ƥ������ɫ��Լ��񣬴�CSS��ƣ���ͼƬ������css3������ǿ'},
	{name: 'mac', about: '��ɫ��Լ���Ƥ������࣬��CSS��ƣ���ͼƬ������css3������ǿ'},
	{name: 'idialog', about: 'ƻ�����Է��ĵ���Ƥ�����ܲ���ģ�����ͼƬ'}
]);

$(function(){
	// ��ť������������
	$(document).bind('click', function(event){
		var target = event.target,
			$target = $(target);

		if ($target.hasClass('runcode')) {
			$('#' + target.name).runCode();
		};
	});
	
	var $skin = $('#demo_skin');
	if (!$skin[0]) return;
	
	$skin.bind('click', function () {
		_demoSkin();
		return false;
	});
});

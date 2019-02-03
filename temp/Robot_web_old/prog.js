var titlePattern;
var htmlTitlePattern;

$(function () {
	titlePattern = $('title').text();
	htmlTitlePattern = $('#htmlTitle').html();

	getTaskInfo();
})

function getTaskInfo(){
	$.get('getTaskInfo.php?task_id=' + getTaskId(),
		function(jsonTaskInfo) {
			setTaskInfo(jsonTaskInfo);
		}
	);
	$.get('getUserProgram.php',
		function(jsonUserProgram) {
			setUserProgram(jsonUserProgram);
		}
	);
}

function getTaskId(){
	return window.location.href.split('?')[1];
}

function setTaskInfo(jsonTaskInfo){
	var task = JSON.parse(jsonTaskInfo).task;

	$('title').text(titlePattern
		.replace('$task_id', task.task_id)
		.replace('$title', task.title));

	$('#htmlTitle').html(htmlTitlePattern
		.replace('$task_id', task.task_id)
		.replace('$title', task.title));

	$('#test_in').val(task.test_in);
	$('#test_out').val(task.test_out);
}

function setUserProgram(jsonUserProgram){
	var prog = JSON.parse(jsonUserProgram).prog;
	$('#program').val(prog.program);
	$('#langButton').html(prog.lang);

}
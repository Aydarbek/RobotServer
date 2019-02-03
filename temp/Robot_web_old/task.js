var htmlPattern;
var titlePattern;

$(function() {
	htmlPattern = $('body').html();
	titlePattern = $('title').html();

	getTaskInfo();
})

function getTaskInfo(){
	$.get('getTaskInfo.php?task_id=' + getTaskId(),
		function(jsonTaskInfo) {
			setTaskInfo(jsonTaskInfo);
		}
	);
}

function getTaskId(){
	return window.location.href.split('?')[1];
}


function setTaskInfo(jsonTaskInfo){
	var task = JSON.parse(jsonTaskInfo).task;

	var htmlOutput = htmlPattern
		.replace('$task_id', task.task_id)
		.replace('$title', task.title)
		.replace('$problem', task.problem)
		.replace('$video', task.video);
	$('body').html(htmlOutput);

	var titleOutput = titlePattern
		.replace('$task_id', task.task_id)
		.replace('$title', task.title);
	$('title').html(titleOutput);

	$('#test_in').val(task.test_in);
	$('#test_out').val(task.test_out);

}
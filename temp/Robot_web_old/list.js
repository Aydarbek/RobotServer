var oneTaskItem;

$(function() {
    oneTaskItem = $('#table > tbody').html();
    getTaskList();
});

function getTaskList(){
    $.get('getTaskList.php',
        function(jsonTaskList) {
            addTaskRows(jsonTaskList);
        }
    );
}

function addTaskRows(jsonTaskList) {
    $('#table > tbody').html("");
    var obj = JSON.parse(jsonTaskList);
    for (var j = 0; j < obj.tasks.length; j++)
        addTaskRow(
            obj.tasks[j].task_id, 
            obj.tasks[j].title);
}

function addTaskRow(task_id, title){
    $('#table > tbody').append(
        oneTaskItem
            .replace(/\$task_id/g, task_id)
            .replace('$title', title));
    }


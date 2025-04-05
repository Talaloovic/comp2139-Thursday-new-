function loadComment(projectId) {
    $.ajax({
        url: '/ProjectManagement/ProjectComment/GetComments?projectId=' + projectId,
        method: 'GET',
        success: function (data) {
            var commentsHtml = '';
            for (var i = 0; i < data.length; i++) {
                commentsHtml += '<div class="comments">';
                commentsHtml += '<p>' + data[i].content + '</p>';
                commentsHtml += '<span>Posted On:' + new Data([i].DatatPosted).toLocaleString() + '</span>';
                commentsHtml += '</div>';
            }
            $('#commentsList').html(commentsHtml);
        }
    });
    
}

$(document).ready(function () {
    // load comment
   var projectId = $('#projectComments input[name="ProjectId"]').val();
   loadComment(projectId);
   
   /// add comment
    $('#addCommentForm').submit(function (evt) {
       evt.preventDefault();
       var formData ={
           'projectId' : projectId,
           content: $('#projectComment textarea[name="ProjectId"]').val()
       };
       
       
       $.ajax({
           url: '/ProjectManagement/ProjectComment/AddComment',
           method: 'POST',
           contentType: application/jason,
           data:JSON.stringify(formData),
           success: function (response) {
               
               if (response.success) {
                   $('#projectComment textarea[name="ProjectId"]').val('')
                   loadComment(projectId);
               }else {
                   alert(response.message);
                   
               }
           },
           error: function (xhr, status, error) {
               alert("Error: " + xhr.responseText);
           }
       });
        
            
    });
   
   
});
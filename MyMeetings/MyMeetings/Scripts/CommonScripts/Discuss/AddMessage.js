$(function AddMessage() {
    var data = { chatId: $("#chat-id").val(), message:$("#message").val};
    $.post("Discuss/Addmessage?",data)
  
});
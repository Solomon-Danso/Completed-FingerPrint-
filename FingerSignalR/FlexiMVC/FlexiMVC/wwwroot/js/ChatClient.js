var connectionUserCount = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connectionUserCount.on("ReceiveMessage", (user, message) => {

    var sender = document.getElementById("senderMessage")
    sender.innerText = user.toString();

    var msg = document.getElementById("Message")
    msg.innerText = message.toString();

   // var imgElement = document.getElementById("imageElement");
   // imgElement.src = "data:image/bmp;base64," + base64String;

})

let connId;




connectionUserCount.on("ConnectionId", (connectionId) => {
    var connectionIdElement = document.getElementById("connectionId");
    connectionIdElement.innerText = connectionId.toString(); 
    connId = connectionId.toString()
   


    var user = prompt("Enter your UserId");
   

    fetch(" http://localhost:5286/api/Chat/UserOnboard?UserId="+user+"&ConnectionId="+connectionId.toString(),{
        method: 'POST',
           
    })
.then(response => response.text()) // Assuming the response is a text (base64 string)
.then(data => {
 
console.log("Api sent")

})
.catch(error => {
  console.error('Error fetching API:', error);
});





});



connectionUserCount.on("DisConnectionId", (connectionId) => {
   
    fetch(" http://localhost:5286/api/Chat/DeleteOneUser?ConnectionId="+connectionId.toString(),{
        method: 'DELETE',
           
    })
.then(response => response.text()) // Assuming the response is a text (base64 string)
.then(data => {
 
console.log("Api DELETE SUCCESS")

})
.catch(error => {
  console.error('Error fetching API:', error);
});



});






connectionUserCount.on("ApiLink", (ApiLink) => {

    var apiLink = document.getElementById("apiLink")
    apiLink.innerText = ApiLink.toString();  


    // Fetch the API endpoint
fetch(ApiLink.toString())
.then(response => response.text()) // Assuming the response is a text (base64 string)
.then(base64String => {
 
   var imgElement = document.getElementById("imageElement");
    imgElement.src = "data:image/bmp;base64," + base64String;


})
.catch(error => {
  console.error('Error fetching API:', error);
});





})







function newWindowLoadedOnClient() {
    connectionUserCount.send("SendApiLink");
}


//start connection
function fulfilled() {
    console.log("Connection to user hub is successful")
    newWindowLoadedOnClient()
}

function rejected() {
    console.log("Connection to user hub rejected")
}


connectionUserCount.start().then(fulfilled, rejected)




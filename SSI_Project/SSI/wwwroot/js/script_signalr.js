// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
"use strict"
// build hub server trong js(sử dụng signalr)
var connection = new signalR.HubConnectionBuilder().withUrl("/hub").build();
//lắng nghe sự kiện khi hàm reload data được gọi:create, update, delete 
connection.on("ReloadData", () => {
    location.reload();
})
//khởi động hub server 
connection.start().then().catch(err=>console.error(err));
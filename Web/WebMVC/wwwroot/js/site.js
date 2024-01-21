// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
async function connectHub() {
    let connection = new signalR.HubConnectionBuilder()
        .withUrl('/hub/notificationhub')
        .withAutomaticReconnect()
        .build();

    await connection.start();

    connection.on("UpdatedOrderStatus", () => {
        if (window.location.pathname.split("/").pop() === 'Order') {
            window.location.reload();
        }
    });
}

let eventsArr = [];
let EventsTable = document.getElementById("EventsTable");
let trElments = EventsTable.getElementsByTagName("tr");
for (let tr of trElments) {
    let tdElments = tr.getElementsByTagName("td");
    let Eventobj = {
        title: tdElments[0].innerText,
        start: tdElments[1].innerText,
        end: tdElments[2].innerText,
        description: tdElments[3].innerText,
        id: tdElments[4].innerText
    };
    eventsArr.push(Eventobj);
}
console.log(eventsArr);
document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('calendar');
    var calendar = new FullCalendar.Calendar(calendarEl, {
        dayMaxEventRows: true, // for all non-TimeGrid views
        views: {
            timeGrid: {
                dayMaxEventRows: 3 // adjust to 6 only for timeGridWeek/timeGridDay
            }
        },
        initialView: 'dayGridMonth',
        height: 700,
        headerToolbar: {
            left: 'prevYear,prev,today,next,nextYear',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay,listMonth'
        },
        events: eventsArr,
        eventClick: function (info) {
            var item_id = info.event.id
            Swal.fire({
                title: 'What you want to do with this event!',
                text: "If you delete it you won't be able to revert this again!",
                type: 'warning',
                showDenyButton: true,
                showCancelButton: true,
                confirmButton: true,
                denyButtonText: 'Edit',
                denyButtonColor: '#28a745',
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, delete it!'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        method: "POST",
                        url: "/Calendereds/Delete",
                        data: { Id: item_id }
                    }).done(function () {
                        console.log("Where are you bb?");
                        Swal.fire(
                            'Deleted!',
                            'Your file has been deleted.',
                            'success'
                        ).then((result) => {
                            info.event.remove();
                            location.reload();
                        });
                    });
                } else if (result.isDenied) {
                    $('#EditEvent').modal('toggle');
                    var startid = info.event.id + " + s";
                    var endid = info.event.id + " + e";
                    document.getElementById("EditTitle").value = info.event.title;
                    document.getElementById("EditStart").value = document.getElementById(startid).innerText;
                    document.getElementById("EditEnd").value = document.getElementById(endid).innerText;
                    $('#EditDone').click(function () {
                        var ItemStart = document.getElementById("EditStart").value;
                        var ItemTitle = document.getElementById("EditTitle").value;
                        var ItemEnd = document.getElementById("EditEnd").value;
                        $.ajax({
                            method: "POST",
                            url: "/Calendereds/Edit",
                            data: { Id: item_id, start: ItemStart, end: ItemEnd, title: ItemTitle },
                            success: location.reload()
                        }).location.reload();
                    });
                }
            });
        },
    });
    calendar.render();
});

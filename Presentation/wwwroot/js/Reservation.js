document.addEventListener('DOMContentLoaded', () => {
    const calendar = new VanillaCalendar('#calendar', {
        type: 'default',
        settings: {
            lang: 'en',
        },
    });
    try {
        calendar.init();
        console.log('Calendar initialized successfully.');
    } catch (error) {
        console.error('Error initializing calendar:', error);
    }

    let calendarContainer = document.querySelector('#calendar')
    calendarContainer.addEventListener('click', async (event) => {
        let selectedbtn = calendarContainer.querySelector('.vanilla-calendar-day__btn_selected');
        if (selectedbtn != null) {
        const selectedDate = selectedbtn.getAttribute('data-calendar-day');
            if (selectedDate) {
                await fetchReservations(selectedDate);
            }
            
        }
        
    });

    document.querySelector('.close').addEventListener('click', closeModal);
    document.querySelector('.btn-cancel').addEventListener('click', closeModal);


    injectReservationCellEvents();



   
});


const fetchReservations = async (date) => {
    try {
        console.log("fetching data: ", date);
        const response = await fetch(`/api/ReservationApi?date=${date}`);
        if (!response.ok) throw new Error('Failed to fetch reservations.');

        const reservations = await response.json();

        const reservationBody = document.getElementById('reservationBody');
        reservationBody.innerHTML = '';

        for (let hour = 6; hour <= 21; hour++) {
            const row = document.createElement('tr');
            const timeCell = document.createElement('td');
            timeCell.textContent = `${hour}:00`;
            row.appendChild(timeCell);

            for (const courtId in reservations) {
                const cell = document.createElement('td');
                cell.classList.add('reservation-cell');

                cell.setAttribute('data-courtid', courtId);

                const reservation = reservations[courtId].find(r => new Date(r.startTime).getHours() === hour);

                if (reservation) {
                    cell.textContent = 'Booked';
                    cell.classList.add('reservation');
                    cell.setAttribute('data-userid', reservation.userId);
                    cell.setAttribute('data-reservationid', reservation.id);
                    
                }
                row.appendChild(cell);
            }

            reservationBody.appendChild(row);
        }
    } catch (error) {
        console.error('Error updating table:', error);
    }
    finally {
        injectReservationCellEvents();
    }
};

const injectReservationCellEvents = () => {
    //const cells = document.querySelectorAll('.reservation-cell');
    //cells.forEach(cell => {
    //    cell.addEventListener('click', onReservationCellClick);
    //});

    document.querySelectorAll(".reservation-cell").forEach(cell => {
        cell.addEventListener("click", function () {
            const reservationId = this.dataset.reservationid; 
            const userId = this.dataset.userid;
            const courtId = this.dataset.courtId;


            let calendarContainer = document.querySelector('#calendar');
            let selectedbtn = calendarContainer.querySelector('.vanilla-calendar-day__btn_selected');
            let selectedDate;
            if (selectedbtn != null) {
                selectedDate = selectedbtn.getAttribute('data-calendar-day');

            }
            else {
                const today = new Date();
                const result = new Date(today.getFullYear(), today.getMonth(), today.getDay());
                selectedDate = result.toISOString().split('T')[0];
                openModal(courtId, selectedDate);
            }
            if (selectedDate) {
                console.log('Cell clicked:', event.target);
                console.log('Cell clicked data:', selectedDate);
            }



            if (reservationId) {
                const form = document.createElement("form");
                form.action = "/Reservation"; 
                form.method = "post";

                const antiForgeryTokenInput = document.createElement("input");
                antiForgeryTokenInput.type = "hidden";
                antiForgeryTokenInput.name = "__RequestVerificationToken";
                antiForgeryTokenInput.value = document.querySelector('input[name="__RequestVerificationToken"]').value;
                form.appendChild(antiForgeryTokenInput);

                const reservationIdInput = document.createElement("input");
                reservationIdInput.type = "hidden";
                reservationIdInput.name = "reservationId";
                reservationIdInput.value = reservationId;
                form.appendChild(reservationIdInput);

                const authTokenInput = document.createElement("input");
                authTokenInput.type = "hidden";
                authTokenInput.name = "token";
                authTokenInput.value = localStorage.getItem("authToken");
                form.appendChild(authTokenInput);

                document.body.appendChild(form);
                form.submit();
            }
        });
    });
};


function openModal(courtId, selectedDate) {
    const backdrop = document.getElementById('backdrop-reservation');
    const modal = document.getElementById('reservationModal');

    document.getElementById('modalCourtId').textContent = courtId;
    document.getElementById('modalSelectedDate').textContent = selectedDate;

    document.getElementById('hiddenCourtId').value = courtId;
    document.getElementById('hiddenSelectedDate').value = selectedDate;

    backdrop.classList.add('show');
    modal.classList.add('show');
}

function closeModal() {
    const backdrop = document.getElementById('backdrop-reservation');
    const modal = document.getElementById('reservationModal');

    backdrop.classList.remove('show');
    modal.classList.remove('show');
}



//const onReservationCellClick = (event) => {
//    let calendarContainer = document.querySelector('#calendar');
//    let selectedbtn = calendarContainer.querySelector('.vanilla-calendar-day__btn_selected');
//    if (selectedbtn != null) {
//        let selectedDate = selectedbtn.getAttribute('data-calendar-day');
//        if (selectedDate) {
//            console.log('Cell clicked:', event.target);
//            console.log('Cell clicked data:', selectedDate);
//        }
//    }






//};


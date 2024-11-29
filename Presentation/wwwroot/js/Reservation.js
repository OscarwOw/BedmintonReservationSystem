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
    const cells = document.querySelectorAll('.reservation-cell');
    cells.forEach(cell => {
        cell.addEventListener('click', onReservationCellClick);
    });
};

const onReservationCellClick = (event) => {
    let calendarContainer = document.querySelector('#calendar');
    let selectedbtn = calendarContainer.querySelector('.vanilla-calendar-day__btn_selected');
    if (selectedbtn != null) {
        let selectedDate = selectedbtn.getAttribute('data-calendar-day');
        if (selectedDate) {
            console.log('Cell clicked:', event.target);
            console.log('Cell clicked data:', selectedDate);
        }
    }

};


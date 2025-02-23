import { useEffect, useState } from 'react';
import './App.css';
import { Dialog } from '@mui/material';

interface Resource {
    id: number;
    name: string;
}

interface BookingFormData {
    dateFrom: string;
    dateTo: string;
    bookedQuantity: number;
}

function App() {
    const [resources, setResources] = useState<Resource[]>([]);
    const [openDialog, setOpenDialog] = useState(false);
    const [selectedResource, setSelectedResource] = useState<Resource | null>(null);
    const [bookingData, setBookingData] = useState<BookingFormData>({
        dateFrom: '',
        dateTo: '',
        bookedQuantity: 1
    });

    useEffect(() => {
        fetchResources();
    }, []);

    async function fetchResources() {
        try {
            const response = await fetch('/api/resources');
            const data = await response.json();
            setResources(data);
        } catch (error) {
            console.error("Error fetching resources:", error);
        }
    }

    function handleOpenDialog(resource: Resource) {
        setSelectedResource(resource);
        setOpenDialog(true);
    }

    function handleCloseDialog() {
        setOpenDialog(false);
        setSelectedResource(null);
        clearForm();
    }

    function handleBookingChange(event: React.ChangeEvent<HTMLInputElement>) {
        setBookingData({
            ...bookingData,
            [event.target.name]: event.target.value
        });
    }

    async function handleBook() {
        if (!selectedResource) return;

        try {
            const response = await fetch(`/api/resources/${selectedResource.id}/bookings`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(bookingData)
            });

            const result = await response.json(); 

            if (response.ok) {
                alert(result.message);
                handleCloseDialog();
            } else {
                alert(result.message || "Booking failed. Please try again.");
            }
        } catch (error: any) {
            alert("An unexpected error occurred: " + error.message);
        }
    }

    function clearForm() {
        setBookingData({
            dateFrom: '',
            dateTo: '',
            bookedQuantity: 1
        });
    }

    return (
        <div>
            <h1>Resources</h1>
            <table className="table table-striped">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Name</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody>
                    {resources.map(resource => (
                        <tr key={resource.id}>
                            <td>{resource.id}</td>
                            <td>{resource.name}</td>
                            <td><button onClick={() => handleOpenDialog(resource)}>Book Here</button></td>
                        </tr>
                    ))}
                </tbody>
            </table>

            <Dialog open={openDialog} onClose={handleCloseDialog}>
                <div className="dialog-content">
                    <h2>Book {selectedResource?.name}</h2>
                    <div className="form-container">
                        <label>Date From:</label>
                        <input type="datetime-local" name="dateFrom" value={bookingData.dateFrom} onChange={handleBookingChange} />
                        <label>Date To:</label>
                        <input type="datetime-local" name="dateTo" value={bookingData.dateTo} onChange={handleBookingChange} />
                        <label>Quantity:</label>
                        <input type="number" name="bookedQuantity" value={bookingData.bookedQuantity} onChange={handleBookingChange} min={1} />
                    </div>
                    <div className="button-group">
                        <button onClick={handleBook}>Book</button>
                        <button onClick={handleCloseDialog}>Cancel</button>
                    </div>
                </div>
            </Dialog>
        </div>
    );
}

export default App;

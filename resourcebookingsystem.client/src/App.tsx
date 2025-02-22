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
    quantity: number;
}

function App() {
    const [resources, setResources] = useState<Resource[]>([]);
    const [openDialog, setOpenDialog] = useState(false);
    const [selectedResource, setSelectedResource] = useState<Resource | null>(null);
    const [bookingData, setBookingData] = useState<BookingFormData>({
        dateFrom: '',
        dateTo: '',
        quantity: 1
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
    }

    function handleBookingChange(event: React.ChangeEvent<HTMLInputElement>) {
        setBookingData({
            ...bookingData,
            [event.target.name]: event.target.value
        });
    }

    async function handleBook() {
        if (!selectedResource) return;
        const booking = { ...bookingData, resourceId: selectedResource.id };
        try {
            const response = await fetch('/bookings', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(booking)
            });
            if (response.ok) {
                alert("Booking successful!");
                handleCloseDialog();
            } else {
                alert("Booking failed. Please try again.");
            }
        } catch (error) {
            console.error("Error submitting booking:", error);
        }
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
                    <label>Date From:</label>
                    <input type="datetime-local" name="dateFrom" value={bookingData.dateFrom} onChange={handleBookingChange} />
                    <label>Date To:</label>
                    <input type="datetime-local" name="dateTo" value={bookingData.dateTo} onChange={handleBookingChange} />
                    <label>Quantity:</label>
                    <input type="number" name="quantity" value={bookingData.quantity} onChange={handleBookingChange} min={1} />
                    <button onClick={handleBook}>Book</button>
                    <button onClick={handleCloseDialog}>Cancel</button>
                </div>
            </Dialog>
        </div>
    );
}

export default App;

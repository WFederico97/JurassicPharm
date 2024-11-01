
async function fetchEmployeesByStore() {
    const token = localStorage.getItem('jwtToken'); 
    try {
        const response = await fetch('https://localhost:7289/GetStores',{
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        });
        if (!response.ok) throw new Error('Error al obtener las sucursales');

        const stores = await response.json();
        const tableBody = document.querySelector('#employeeTable tbody');
        tableBody.innerHTML = ''; 

        stores.forEach(store => {
            
            const activeEmployees = store.empleados.filter(emp => emp.active);
            const activeAdmins = store.empleados.filter(emp => emp.active && emp.role === 'ADMIN');
            const activeRepositores = store.empleados.filter(emp => emp.active && emp.role === 'REPOSITOR');
            const activeCashiers = store.empleados.filter(emp => emp.active && emp.role === 'CAJERO');
            console.log(activeEmployees)
            if (activeEmployees.length > 0) {
                const row = document.createElement('tr');
                row.innerHTML = `
                    <td>${store.calle}, ${store.altura}</td>
                    <td>${activeEmployees.map(emp => `${emp.nombre} ${emp.apellido}`).join(', ')}</td>
                    <td>${activeEmployees.length}</td>
                    <td>${activeAdmins.length}</td>
                    <td>${activeCashiers.length}</td>
                    <td>${activeRepositores.length}</td>
                `;
                tableBody.appendChild(row);
            }
        });
    } catch (error) {
        console.error('Error fetching stores:', error);
    }
}

async function fetchInvoices() {
    const token = localStorage.getItem('jwtToken'); 
    try {
        const response = await fetch('https://localhost:7289/api/invoice',{
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        });
        if (!response.ok) throw new Error('Error al obtener facturas');

        const invoices = await response.json();
        createSalesChart(invoices);
    } catch (error) {
        console.error('Error fetching invoices:', error);
    }
}

function createSalesChart(invoices) {
    const ctx = document.getElementById('salesChart').getContext('2d');
    const labels = invoices.map(invoice => new Date(invoice.date).toLocaleDateString());
    const data = invoices.map(invoice => 
        invoice.details.reduce((total, detail) => total + detail.unitPrice * detail.amount, 0)
    );

    new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: 'Ventas de medicamentos',
                data: data,
                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                borderColor: 'rgba(75, 192, 192, 1)',
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
}

document.addEventListener('DOMContentLoaded', () => {
    fetchEmployeesByStore();
    fetchInvoices();
});

document.getElementById('add-sale-form').addEventListener('submit', async (e) => {
    e.preventDefault();
    const token = localStorage.getItem('jwtToken'); 
    const formData = {
        clientId: document.getElementById('clientId').value,
        branchId: document.getElementById('branchId').value,
        date: document.getElementById('date').value,
        details: [] 
    };

    try {
        const response = await fetch('https://localhost:7289/api/invoice', {
            method: 'POST',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(formData)
        });

        if (response.ok) {
            showAlert('Venta agregada con éxito', 'success');
        } else {
            showAlert('Error al agregar la venta', 'danger');
        }
    } catch (error) {
        console.error('Error al enviar los datos:', error);
    }
});

document.getElementById('addSupplyButton').addEventListener('click', () => {
    const selectedSupplyId = supplySelect.value;
    const selectedSupply = supplies.find(supply => supply.id == selectedSupplyId);

    if (selectedSupply) {
        const listItem = document.createElement('li');
        listItem.textContent = `${selectedSupply.name} - $${selectedSupply.unitPrice}`;

        document.getElementById('selectedSuppliesList').appendChild(listItem);
        
    }
});

const supplies = [
    { id: 1, name: 'Suministro 1', unitPrice: 100 },
    { id: 2, name: 'Suministro 2', unitPrice: 200 },
    { id: 3, name: 'Suministro 3', unitPrice: 150 },
];

const supplySelect = document.getElementById('supplySelect');
supplies.forEach(supply => {
    const option = document.createElement('option');
    option.value = supply.id;
    option.textContent = `${supply.name} - $${supply.unitPrice}`;
    supplySelect.appendChild(option);
});

function showAlert(message, type = 'info') {
    const alertContainer = document.getElementById('alertContainer');
    
    const alertDiv = document.createElement('div');
    alertDiv.className = `alert alert-${type} alert-dismissible fade show`;
    alertDiv.role = 'alert';
    alertDiv.innerHTML = `
        ${message}
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    `;

    alertContainer.appendChild(alertDiv);

    setTimeout(() => {
        alertDiv.classList.remove('show');
        alertDiv.classList.add('hide');
        setTimeout(() => alertDiv.remove(), 500); 
    }, 5000);
}
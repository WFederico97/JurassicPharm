import { showAlert } from "../helpers/showAlert.js";

async function fetchEmployeesByStore() {
    const token = localStorage.getItem('jwtToken'); 
    try {
        const response = await fetch('https://localhost:3000/api/GetStores',{
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

            if (activeEmployees.length > 0) {
                const row = document.createElement('tr');
                row.innerHTML = `
                    <td>${store.provincia}</td>
                    <td>${store.localidad}</td>
                    <td>${store.ciudad}</td>
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
        createStoreChart(stores);
    } catch (error) {
        console.error('Error fetching stores:', error);
    }
}

async function fetchInvoices() {
    const token = localStorage.getItem('jwtToken'); 
    try {
        const response = await fetch('https://localhost:3000/api/invoice',{
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        });
        if (!response.ok) throw new Error('Error al obtener facturas');

        const invoices = await response.json();
        createSalesChart(invoices);
        createSalesBySupplyChart(invoices);
    } catch (error) {
        console.error('Error fetching invoices:', error);
    }
}

function createSalesBySupplyChart(invoices) {
    const salesBySupply = {};

    // process each invoice
    invoices.forEach(invoice => {
        invoice.details.forEach(detail => {
            const supplyName = detail.supplyName; // Use the supply name as the key
            const totalSales = detail.unitPrice * detail.amount; // Calculate total sales for this detail
            if (salesBySupply[supplyName]) {
                salesBySupply[supplyName] += totalSales;
            } else {
                salesBySupply[supplyName] = totalSales;
            }
        });
    });

    // Take the keys of the salesBySupply object as the labels
    const supplies = Object.keys(salesBySupply);
    const data = supplies.map(supply => salesBySupply[supply]);

    const ctx = document.getElementById('salesBySupplyChart').getContext('2d');
    new Chart(ctx, {
        type: 'bar',
        data: {
            labels: supplies, // Use the supply names as labels
            datasets: [{
                label: 'Facturacion por suministro ($)',
                data: data,
                backgroundColor: '#d17d0f',
                borderColor: '#d17d0f',
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            scales: {
                y: {
                    beginAtZero: true
                }
            },
            plugins: {
                tooltip: {
                    callbacks: {
                        label: function(context) {
                            return `Facturacion: $${context.raw.toFixed(2)}`;
                        }
                    }
                }
            }
        }
    });
}


function createSalesChart(invoices) {
    const ctx = document.getElementById('salesChart').getContext('2d');

    // set up yearly sales object
    const yearlySales = {};

    invoices.forEach(invoice => {
        const date = new Date(invoice.date);
        const year = date.getFullYear();

        // calculate total for invoice
        const total = invoice.details.reduce((sum, detail) => sum + detail.unitPrice * detail.amount, 0);

        // add total to yearly sales
        if (yearlySales[year]) {
            yearlySales[year] += total;
        } else {
            yearlySales[year] = total;
        }
    });

    // sort labels and data
    const labels = Object.keys(yearlySales).sort((a, b) => a - b);

    // create data array
    const data = labels.map(label => yearlySales[label]);

    let delayed; // for debouncing

    new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: 'Ventas de medicamentos por año',
                data: data,
                backgroundColor: '#008000',
                borderColor: '#008000',
                borderWidth: 2,
                fill: false,
                tension: 0
            }]
        },
        options: {
            animation: {
                onComplete: () => {
                  delayed = true;
                },
                delay: (context) => {
                    let delay = 0;
                    if (context.type === 'data' && context.mode === 'default' && !delayed) {
                    delay = context.dataIndex * 300 + context.datasetIndex * 100;
                }
                    return delay;
                },
            },
            responsive: true,
            scales: {
                y: {
                    beginAtZero: true
                }
            },
            plugins: { // show total sales in tooltip
                tooltip: {
                    callbacks: {
                        label: function(context) {
                            return `Ventas: $${context.raw.toFixed(2)}`;
                        }
                    }
                }
            }
        }
    });
}


function createStoreChart(stores) {
    const ctx = document.getElementById('storeChart').getContext('2d');
    const labels = stores.map(store => `${store.provincia} - ${store.localidad}`);
    const data = stores.map(store => store.empleados.filter(emp => emp.active).length);

    new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: 'Cantidad de empleados',
                data: data,
                backgroundColor: '#007bff',
                borderColor: '#007bff',
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
        const response = await fetch('https://localhost:3000/api/invoice', {
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

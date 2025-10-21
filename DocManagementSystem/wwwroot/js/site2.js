// Global variables
let currentUserRole = 'super-admin'; // Default to super admin for demo
let currentStep = 1;
let currentPatientStep = 1;
let charts = {};



// Initialize application
document.addEventListener('DOMContentLoaded', function () {
    console.log('MedixPro System Initializing...');
    initializeUserRole();
    setupEventListeners();
    populateAllSections();
    initializeCharts();
    setupRealTimeFeatures();
    initializePrescriptionCanvas();
    console.log('MedixPro System Ready');
});

// User Role and Access Control
function initializeUserRole() {
    const welcomeMessage = document.getElementById('welcomeMessage');
    if (welcomeMessage) {
        welcomeMessage.textContent = `Welcome back, ${currentUserRole.replace('-', ' ').toUpperCase()}!`;
    }
    hideRoleRestrictedSections();
}

function hideRoleRestrictedSections() {
    const navItems = document.querySelectorAll('.nav-item');
    // Simplified role-based hiding - super-admin sees all, others limited
    if (currentUserRole !== 'super-admin') {
        const restricted = ['super-admin-dashboard', 'organizations'];
        navItems.forEach(item => {
            if (restricted.includes(item.dataset.section)) {
                item.style.display = 'none';
            }
        });
    }
    showSection('super-admin-dashboard');
}

// Event Listeners Setup
function setupEventListeners() {
    // Navigation
    document.querySelectorAll('.nav-item').forEach(item => {
        item.addEventListener('click', (e) => {
            e.preventDefault();
            showSection(e.currentTarget.dataset.section);
        });
    });

    // Search functionality
    const globalSearch = document.getElementById('globalSearch');
    if (globalSearch) {
        globalSearch.addEventListener('input', debounce(handleGlobalSearch, 300));
    }

    // Mobile sidebar toggle
    const toggleSidebar = document.getElementById('toggleSidebar');
    if (toggleSidebar) {
        toggleSidebar.addEventListener('click', handleToggleSidebar);
    }

    // Notification handling
    const notificationBell = document.getElementById('notificationBell');
    if (notificationBell) {
        notificationBell.addEventListener('click', toggleNotifications);
    }
    document.addEventListener('click', handleOutsideClick);

    // Multi-step modals
    setupDoctorModal();
    setupPatientModal();

    // Form submissions
    setupFormHandlers();

    // Filter buttons
    setupFilterHandlers();

    // Role form
    const roleForm = document.getElementById('roleForm');
    if (roleForm) {
        roleForm.addEventListener('submit', (e) => {
            e.preventDefault();
            showAlert('Permissions updated successfully!', 'success');
        });
    }

    // Window resize handler
    window.addEventListener('resize', handleWindowResize);

    // Keyboard shortcuts
    document.addEventListener('keydown', handleKeyboardShortcuts);
}

// Navigation and Section Management
function showSection(sectionId) {
    document.querySelectorAll('.content-section').forEach(section => {
        section.classList.add('hidden');
    });

    const targetSection = document.getElementById(sectionId);
    if (targetSection) {
        targetSection.classList.remove('hidden');

        document.querySelectorAll('.nav-item').forEach(item => {
            item.classList.remove('active');
        });
        const activeNavItem = document.querySelector(`[data-section="${sectionId}"]`);
        if (activeNavItem) {
            activeNavItem.classList.add('active');
        }
        // Trigger any section-specific initialization
        if (typeof window[`init${sectionId.replace(/-/g, '')}`] === 'function') {
            window[`init${sectionId.replace(/-/g, '')}`]();
        }
    }
}

// Populate All Sections with Mock Data
function populateAllSections() {
    populateOrganizations();
    populateDoctors();
    populatePatients();
    populateAppointments();
    populatePrescriptions();
    populateLaboratory();
    populatePharmacy();
    populateInventory();
    populateAmbulance();
    populateBloodBank();
    populateBilling();
    populateDepartments();
    populateStaff();
    populateDocuments();
    populateReports();
    populateNotifications();
    populateStaffTasks();
    populateCareTips();
    populateActivityTable();
    populateIntegrationsList();
    populateRevenueTable();
    populateTodaySchedule();
}

function populateOrganizations() {
    const tableBody = document.getElementById('organizationsTable');
    if (tableBody) {
        tableBody.innerHTML = mockData.organizations.map(org => `
            <tr>
                <td>${org.name}</td>
                <td>${org.type}</td>
                <td><span class="badge bg-${org.status.toLowerCase()}">${org.tier}</span></td>
                <td>${org.doctors}</td>
                <td>${org.revenue}</td>
                <td><span class="badge bg-${org.status.toLowerCase()}">${org.status}</span></td>
                <td><button class="btn btn-sm btn-primary">Edit</button> <button class="btn btn-sm btn-danger">Delete</button></td>
            </tr>
        `).join('');
    }
}

function populateDoctors() {
    const tableBody = document.getElementById('doctorsTable');
    if (tableBody) {
        tableBody.innerHTML = mockData.doctors.map(doc => `
            <tr>
                <td>${doc.name}</td>
                <td>${doc.specialty}</td>
                <td>${doc.department}</td>
                <td>${doc.patients}</td>
                <td><span class="badge bg-${doc.status.toLowerCase()}">${doc.status}</span></td>
                <td><button class="btn btn-sm btn-primary">Edit</button> <button class="btn btn-sm btn-danger">Deactivate</button></td>
            </tr>
        `).join('');
    }
}

function populatePatients() {
    const tableBody = document.getElementById('patientsTable');
    if (tableBody) {
        tableBody.innerHTML = mockData.patients.map(patient => `
            <tr>
                <td>${patient.name}</td>
                <td>${patient.demographics}</td>
                <td>${patient.history}</td>
                <td>${patient.lastVisit}</td>
                <td>${patient.doctor}</td>
                <td><button class="btn btn-sm btn-info">View EHR</button></td>
            </tr>
        `).join('');
    }
}

function populateAppointments() {
    const tableBody = document.getElementById('appointmentsTable');
    if (tableBody) {
        tableBody.innerHTML = mockData.appointments.map(apt => `
            <tr>
                <td>${apt.patient}</td>
                <td>${apt.doctor}</td>
                <td>${apt.dateTime}</td>
                <td>${apt.duration}</td>
                <td>${apt.type}</td>
                <td><span class="badge bg-success">${apt.status}</span></td>
                <td><button class="btn btn-sm btn-primary">Edit</button> <button class="btn btn-sm btn-danger">Cancel</button></td>
            </tr>
        `).join('');
    }
}

function populatePrescriptions() {
    const tableBody = document.getElementById('prescriptionsTable');
    if (tableBody) {
        tableBody.innerHTML = mockData.prescriptions.map(pres => `
            <tr>
                <td>${pres.patient}</td>
                <td>${pres.medication}</td>
                <td>${pres.dosageFreq}</td>
                <td>${pres.duration}</td>
                <td><span class="badge bg-${pres.pharmacyStatus.toLowerCase()}">${pres.pharmacyStatus}</span></td>
                <td><button class="btn btn-sm btn-primary">${pres.actions}</button></td>
            </tr>
        `).join('');
    }
}

function populateLaboratory() {
    const tableBody = document.getElementById('labTable');
    if (tableBody) {
        tableBody.innerHTML = mockData.laboratory.map(test => `
            <tr>
                <td>${test.patient}</td>
                <td>${test.testType}</td>
                <td>${test.orderedBy}</td>
                <td>${test.dateOrdered}</td>
                <td><span class="badge bg-${test.status.toLowerCase()}">${test.status}</span></td>
                <td><button class="btn btn-sm btn-info">${test.results}</button></td>
            </tr>
        `).join('');
    }
}

function populatePharmacy() {
    const tableBody = document.getElementById('pharmacyTable');
    if (tableBody) {
        tableBody.innerHTML = `
            <tr>
                <td>Lisinopril</td>
                <td>150 units</td>
                <td>2026-03-01</td>
                <td>PKR 50</td>
                <td>Integrated</td>
                <td><button class="btn btn-sm btn-primary">Edit</button></td>
            </tr>
            <tr>
                <td>Metformin</td>
                <td>200 units</td>
                <td>2025-12-15</td>
                <td>PKR 30</td>
                <td>Pending</td>
                <td><button class="btn btn-sm btn-primary">Edit</button></td>
            </tr>
        `;
    }
}

function populateInventory() {
    const tableBody = document.getElementById('inventoryTable');
    if (tableBody) {
        tableBody.innerHTML = mockData.inventory.map(item => `
            <tr>
                <td>${item.item}</td>
                <td>${item.quantity}</td>
                <td>${item.supplier}</td>
                <td>${item.expiry}</td>
                <td><span class="badge bg-${item.alert === 'Low' ? 'warning' : 'success'}">${item.alert}</span></td>
                <td><button class="btn btn-sm btn-primary">${item.actions}</button></td>
            </tr>
        `).join('');
    }
}

function populateAmbulance() {
    const tableBody = document.getElementById('ambulanceTable');
    if (tableBody) {
        tableBody.innerHTML = mockData.ambulance.map(req => `
            <tr>
                <td>${req.id}</td>
                <td>${req.patient}</td>
                <td>${req.location}</td>
                <td><span class="badge bg-info">${req.status}</span></td>
                <td>${req.eta}</td>
                <td><button class="btn btn-sm btn-primary">${req.actions}</button></td>
            </tr>
        `).join('');
    }
}

function populateBloodBank() {
    const tableBody = document.getElementById('bloodBankTable');
    if (tableBody) {
        tableBody.innerHTML = mockData.bloodBank.map(entry => `
            <tr>
                <td>${entry.type}</td>
                <td>${entry.units}</td>
                <td>${entry.donor}</td>
                <td>${entry.expiry}</td>
                <td><span class="badge bg-${entry.status.toLowerCase()}">${entry.status}</span></td>
                <td><button class="btn btn-sm btn-primary">${entry.actions}</button></td>
            </tr>
        `).join('');
    }
}

function populateBilling() {
    const tableBody = document.getElementById('billingTable');
    if (tableBody) {
        tableBody.innerHTML = mockData.billing.map(inv => `
            <tr>
                <td>${inv.id}</td>
                <td>${inv.patient}</td>
                <td>PKR ${inv.amount}</td>
                <td><span class="badge bg-${inv.insurance.toLowerCase()}">${inv.insurance}</span></td>
                <td>${inv.date}</td>
                <td><button class="btn btn-sm btn-primary">${inv.actions}</button></td>
            </tr>
        `).join('');
    }
}

function populateDepartments() {
    const tableBody = document.getElementById('departmentsTable');
    if (tableBody) {
        tableBody.innerHTML = mockData.departments.map(dept => `
            <tr>
                <td>${dept.name}</td>
                <td>${dept.head}</td>
                <td>${dept.staff}</td>
                <td>${dept.allocation}</td>
                <td><span class="badge bg-${dept.status.toLowerCase()}">${dept.status}</span></td>
                <td><button class="btn btn-sm btn-primary">${dept.actions}</button></td>
            </tr>
        `).join('');
    }
}

function populateStaff() {
    const tableBody = document.getElementById('staffTable');
    if (tableBody) {
        tableBody.innerHTML = mockData.staff.map(member => `
            <tr>
                <td>${member.name}</td>
                <td>${member.role}</td>
                <td>${member.department}</td>
                <td>${member.schedule}</td>
                <td><span class="badge bg-${member.status.toLowerCase()}">${member.status}</span></td>
                <td><button class="btn btn-sm btn-primary">${member.actions}</button></td>
            </tr>
        `).join('');
    }
}

function populateDocuments() {
    const tableBody = document.getElementById('documentsTable');
    if (tableBody) {
        tableBody.innerHTML = mockData.documents.map(doc => `
            <tr>
                <td>${doc.name}</td>
                <td>${doc.patient}</td>
                <td>${doc.type}</td>
                <td>${doc.date}</td>
                <td>${doc.access}</td>
                <td><button class="btn btn-sm btn-info">${doc.actions}</button></td>
            </tr>
        `).join('');
    }
}

function populateReports() {
    const tableBody = document.getElementById('reportsTable');
    if (tableBody) {
        tableBody.innerHTML = mockData.reports.map(report => `
            <tr>
                <td>${report.report}</td>
                <td>${report.date}</td>
                <td><span class="badge bg-success">${report.status}</span></td>
                <td><button class="btn btn-sm btn-primary">${report.actions}</button></td>
            </tr>
        `).join('');
    }
}

function populateNotifications() {
    const dropdown = document.getElementById('notificationsDropdown');
    if (dropdown) {
        dropdown.innerHTML = mockData.notifications.map(notif => `
            <div class="notification-item ${notif.read ? 'read' : ''}">
                <div>${notif.message}</div>
                <small class="text-muted">${notif.time}</small>
            </div>
        `).join('');
    }
    const count = document.getElementById('notificationCount');
    if (count) {
        const unread = mockData.notifications.filter(n => !n.read).length;
        count.textContent = unread || '';
        count.style.display = unread > 0 ? 'inline' : 'none';
    }
}

function populateStaffTasks() {
    const tableBody = document.getElementById('staffTasks');
    if (tableBody) {
        tableBody.innerHTML = mockData.staffTasks.map(task => `
            <tr>
                <td>${task.task}</td>
                <td><span class="badge bg-${task.priority.toLowerCase()}">${task.priority}</span></td>
                <td><span class="badge bg-${task.status.toLowerCase()}">${task.status}</span></td>
                <td><button class="btn btn-sm btn-success">${task.actions}</button></td>
            </tr>
        `).join('');
    }
}

function populateCareTips() {
    const list = document.getElementById('careTips');
    if (list) {
        list.innerHTML = mockData.careTips.map(tip => `<li>${tip}</li>`).join('');
    }
}

function populateActivityTable() {
    const tableBody = document.getElementById('activityTable');
    if (tableBody) {
        tableBody.innerHTML = mockData.activityTable.map(row => `
            <tr>
                <td>${row.date}</td>
                <td>${row.steps}</td>
                <td>${row.calories}</td>
                <td><span class="badge bg-success">${row.progress}</span></td>
            </tr>
        `).join('');
    }
}

function populateIntegrationsList() {
    const list = document.getElementById('integrationsList');
    if (list) {
        list.innerHTML = mockData.integrationsList.map(integ => `<li>${integ}</li>`).join('');
    }
}

function populateRevenueTable() {
    const tableBody = document.getElementById('revenueTable');
    if (tableBody) {
        tableBody.innerHTML = mockData.revenueTable.map(row => `
            <tr>
                <td>${row.org}</td>
                <td>${row.revenue}</td>
                <td><span class="badge bg-${row.status.toLowerCase()}">${row.status}</span></td>
            </tr>
        `).join('');
    }
}

function populateTodaySchedule() {
    const tableBody = document.getElementById('todaySchedule');
    if (tableBody) {
        tableBody.innerHTML = mockData.todaySchedule.map(sched => `
            <tr>
                <td>${sched.patient}</td>
                <td>${sched.time}</td>
                <td>${sched.duration}</td>
                <td><span class="badge bg-info">${sched.type}</span></td>
            </tr>
        `).join('');
    }
}

// Charts Initialization
function initializeCharts() {
    // Onboarding Chart
    const onboardingCtx = document.getElementById('onboardingChart');
    if (onboardingCtx) {
        charts.onboarding = new Chart(onboardingCtx, {
            type: 'line',
            data: {
                labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep'],
                datasets: [{
                    label: 'New Clients',
                    data: [12, 19, 3, 5, 2, 3, 7, 12, 5],
                    borderColor: 'rgb(75, 192, 192)',
                    tension: 0.1
                }]
            },
            options: { responsive: true, scales: { y: { beginAtZero: true } } }
        });
    }

    // Department Chart
    const deptCtx = document.getElementById('departmentChart');
    if (deptCtx) {
        charts.department = new Chart(deptCtx, {
            type: 'doughnut',
            data: {
                labels: ['Cardiology', 'Neurology', 'Pediatrics'],
                datasets: [{
                    data: [45, 30, 25],
                    backgroundColor: ['#007bff', '#28a745', '#ffc107']
                }]
            },
            options: { responsive: true }
        });
    }

    // Patient History Chart
    const patientHistoryCtx = document.getElementById('patientHistoryChart');
    if (patientHistoryCtx) {
        charts.patientHistory = new Chart(patientHistoryCtx, {
            type: 'bar',
            data: {
                labels: ['Jan', 'Feb', 'Mar'],
                datasets: [{
                    label: 'Visits',
                    data: [65, 59, 80],
                    backgroundColor: 'rgba(75, 192, 192, 0.2)',
                    borderColor: 'rgba(75, 192, 192, 1)'
                }]
            },
            options: { responsive: true, scales: { y: { beginAtZero: true } } }
        });
    }

    // Patient Health Chart
    const patientHealthCtx = document.getElementById('patientHealthChart');
    if (patientHealthCtx) {
        charts.patientHealth = new Chart(patientHealthCtx, {
            type: 'line',
            data: {
                labels: ['Week 1', 'Week 2', 'Week 3', 'Week 4'],
                datasets: [{
                    label: 'Blood Pressure',
                    data: [120, 118, 122, 119],
                    borderColor: '#dc3545',
                    tension: 0.1
                }]
            },
            options: { responsive: true, scales: { y: { beginAtZero: false } } }
        });
    }

    // Monitoring Chart
    const monitoringCtx = document.getElementById('monitoringChart');
    if (monitoringCtx) {
        charts.monitoring = new Chart(monitoringCtx, {
            type: 'line',
            data: {
                labels: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri'],
                datasets: [{
                    label: 'Heart Rate',
                    data: [72, 74, 70, 75, 73],
                    borderColor: '#28a745',
                    tension: 0.1
                }]
            },
            options: { responsive: true, scales: { y: { beginAtZero: false } } }
        });
    }

    // Fitness Chart
    const fitnessCtx = document.getElementById('fitnessChart');
    if (fitnessCtx) {
        charts.fitness = new Chart(fitnessCtx, {
            type: 'bar',
            data: {
                labels: ['Steps', 'Calories'],
                datasets: [{
                    data: [10000, 500],
                    backgroundColor: ['#007bff', '#28a745']
                }]
            },
            options: { responsive: true, scales: { y: { beginAtZero: true } } }
        });
    }

    // Revenue Chart
    const revenueCtx = document.getElementById('revenueChart');
    if (revenueCtx) {
        charts.revenue = new Chart(revenueCtx, {
            type: 'line',
            data: {
                labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun'],
                datasets: [{
                    label: 'Revenue (PKR)',
                    data: [120000, 190000, 300000, 500000, 200000, 300000],
                    borderColor: '#007bff',
                    tension: 0.1
                }]
            },
            options: { responsive: true, scales: { y: { beginAtZero: true } } }
        });
    }
}

// Real-Time Features Setup (Mock)
function setupRealTimeFeatures() {
    // Simulate real-time notifications
    setInterval(() => {
        if (Math.random() > 0.8) {
            mockData.notifications.unshift({
                id: Date.now(),
                message: `New update: Patient ${['John', 'Emily'][Math.floor(Math.random() * 2)]} checked in`,
                time: 'Just now',
                read: false
            });
            populateNotifications();
        }
    }, 30000); // Every 30 seconds

    // Simulate data refresh
    setInterval(populateAllSections, 60000); // Every minute
}

// Search Functionality
function handleGlobalSearch(e) {
    const query = e.target.value.toLowerCase();
    // Highlight search results across sections
    document.querySelectorAll('.content-section td, .content-section li').forEach(el => {
        el.innerHTML = el.innerHTML.replace(/<mark>/g, '').replace(/<\/mark>/g, '');
        if (el.textContent.toLowerCase().includes(query)) {
            //el.innerHTML = el.innerHTML.replace(new RegExp(query, 'gi'), '<mark>');    // Patient History Chart
    const patientHistoryCtx = document.getElementById('</mark>');
        }
    });
}

function debounce(func, wait) {
    let timeout;
    return function executedFunction(...args) {
        const later = () => {
            clearTimeout(timeout);
            func(...args);
        };
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
    };
}

// Sidebar Toggle
function handleToggleSidebar() {
    const sidebar = document.getElementById('sidebar');
    const mainContent = document.getElementById('mainContent');
    sidebar.classList.toggle('show');
    mainContent.classList.toggle('expanded');
}

// Notifications
function toggleNotifications(e) {
    e.stopPropagation();
    const dropdown = document.getElementById('notificationsDropdown');
    dropdown.classList.toggle('show');
    // Mark as read
    mockData.notifications.forEach(n => n.read = true);
    populateNotifications();
}

function handleOutsideClick(e) {
    const dropdown = document.getElementById('notificationsDropdown');
    const bell = document.getElementById('notificationBell');
    if (!dropdown.contains(e.target) && !bell.contains(e.target)) {
        dropdown.classList.remove('show');
    }
}

// Multi-Step Modals
function setupDoctorModal() {
    const nextBtn = document.getElementById('nextStep');
    const prevBtn = document.getElementById('prevStep');
    const saveBtn = document.getElementById('saveDoctor');
    const stepper = document.getElementById('doctorStepper');

    if (!nextBtn || !prevBtn || !saveBtn || !stepper) return;

    nextBtn.addEventListener('click', () => {
        if (currentStep < 3) {
            document.querySelector(`[data-step="${currentStep}"]`).classList.add('hidden');
            document.querySelector(`[data-step="${currentStep}"] .step`).classList.remove('active');
            currentStep++;
            document.querySelector(`[data-step="${currentStep}"]`).classList.remove('hidden');
            document.querySelector(`[data-step="${currentStep}"] .step`).classList.add('active');
            if (currentStep === 3) {
                saveBtn.style.display = 'inline-block';
                nextBtn.style.display = 'none';
            }
            prevBtn.style.display = currentStep > 1 ? 'inline-block' : 'none';
        }
    });

    prevBtn.addEventListener('click', () => {
        if (currentStep > 1) {
            document.querySelector(`[data-step="${currentStep}"]`).classList.add('hidden');
            document.querySelector(`[data-step="${currentStep}"] .step`).classList.remove('active');
            currentStep--;
            document.querySelector(`[data-step="${currentStep}"]`).classList.remove('hidden');
            document.querySelector(`[data-step="${currentStep}"] .step`).classList.add('active');
            saveBtn.style.display = currentStep === 3 ? 'inline-block' : 'none';
            nextBtn.style.display = currentStep < 3 ? 'inline-block' : 'none';
            prevBtn.style.display = currentStep > 1 ? 'inline-block' : 'none';
        }
    });

    saveBtn.addEventListener('click', () => {
        showAlert('Doctor added successfully!', 'success');
        const modalElement = document.getElementById('addDoctorModal');
        if (modalElement && typeof bootstrap !== 'undefined') {
            bootstrap.Modal.getInstance(modalElement).hide();
        }
        currentStep = 1;
        // Reset stepper
        stepper.querySelectorAll('.step').forEach((step, idx) => {
            step.classList.toggle('active', idx + 1 === 1);
            step.classList.toggle('completed', idx + 1 < 1);
        });
        document.querySelectorAll('.step-content').forEach(content => content.classList.add('hidden'));
        document.querySelector('[data-step="1"]').classList.remove('hidden');
        nextBtn.style.display = 'inline-block';
        saveBtn.style.display = 'none';
        prevBtn.style.display = 'none';
        const form = document.getElementById('addDoctorForm');
        if (form) form.reset();
    });
}

function setupPatientModal() {
    const nextBtn = document.getElementById('patientNext');
    const prevBtn = document.getElementById('patientPrev');
    const saveBtn = document.getElementById('savePatient');
    const stepper = document.querySelector('#addPatientModal .stepper');

    if (!nextBtn || !prevBtn || !saveBtn || !stepper) return;

    nextBtn.addEventListener('click', () => {
        if (currentPatientStep < 2) {
            document.querySelector('#addPatientModal [data-step="' + currentPatientStep + '"]').classList.add('hidden');
            const stepEl = document.querySelector('#addPatientModal .step:nth-child(' + currentPatientStep + ')');
            if (stepEl) stepEl.classList.remove('active');
            currentPatientStep++;
            document.querySelector('#addPatientModal [data-step="' + currentPatientStep + '"]').classList.remove('hidden');
            const newStepEl = document.querySelector('#addPatientModal .step:nth-child(' + currentPatientStep + ')');
            if (newStepEl) newStepEl.classList.add('active');
            if (currentPatientStep === 2) {
                saveBtn.style.display = 'inline-block';
                nextBtn.style.display = 'none';
            }
            prevBtn.style.display = currentPatientStep > 1 ? 'inline-block' : 'none';
        }
    });

    prevBtn.addEventListener('click', () => {
        if (currentPatientStep > 1) {
            document.querySelector('#addPatientModal [data-step="' + currentPatientStep + '"]').classList.add('hidden');
            const stepEl = document.querySelector('#addPatientModal .step:nth-child(' + currentPatientStep + ')');
            if (stepEl) stepEl.classList.remove('active');
            currentPatientStep--;
            document.querySelector('#addPatientModal [data-step="' + currentPatientStep + '"]').classList.remove('hidden');
            const newStepEl = document.querySelector('#addPatientModal .step:nth-child(' + currentPatientStep + ')');
            if (newStepEl) newStepEl.classList.add('active');
            saveBtn.style.display = currentPatientStep === 2 ? 'inline-block' : 'none';
            nextBtn.style.display = currentPatientStep < 2 ? 'inline-block' : 'none';
            prevBtn.style.display = currentPatientStep > 1 ? 'inline-block' : 'none';
        }
    });

    saveBtn.addEventListener('click', () => {
        showAlert('Patient registered successfully!', 'success');
        const modalElement = document.getElementById('addPatientModal');
        if (modalElement && typeof bootstrap !== 'undefined') {
            bootstrap.Modal.getInstance(modalElement).hide();
        }
        currentPatientStep = 1;
        // Reset stepper
        stepper.querySelectorAll('.step').forEach((step, idx) => {
            step.classList.toggle('active', idx + 1 === 1);
        });
        document.querySelectorAll('#addPatientModal .step-content').forEach(content => content.classList.add('hidden'));
        document.querySelector('#addPatientModal [data-step="1"]').classList.remove('hidden');
        nextBtn.style.display = 'inline-block';
        saveBtn.style.display = 'none';
        prevBtn.style.display = 'none';
        const form = document.getElementById('addPatientForm');
        if (form) form.reset();
    });
}

// Form Handlers
function setupFormHandlers() {
    // Generic form handler for modals
    const forms = document.querySelectorAll('form[id$="Form"]');
    forms.forEach(form => {
        form.addEventListener('submit', (e) => {
            e.preventDefault();
            // Simulate form submission
            showAlert(`${form.id.replace('Form', '')} saved successfully!`, 'success');
            const modal = form.closest('.modal');
            if (modal && typeof bootstrap !== 'undefined') {
                const modalInstance = bootstrap.Modal.getInstance(modal);
                if (modalInstance) modalInstance.hide();
            }
            form.reset();
        });
    });
}

// Filter Handlers
function setupFilterHandlers() {
    // Generic filter function
    window.filterOrganizations = () => {
        const search = document.getElementById('orgSearch');
        const status = document.getElementById('orgStatus');
        if (search && status) {
            filterTable('organizationsTable', search.value, status.value);
        }
    };
    window.filterDoctors = () => {
        const search = document.getElementById('doctorSearch');
        const status = document.getElementById('doctorStatus');
        if (search && status) {
            filterTable('doctorsTable', search.value, status.value);
        }
    };
    window.filterPatients = () => {
        const search = document.getElementById('patientSearch');
        const status = document.getElementById('patientStatus');
        if (search && status) {
            filterTable('patientsTable', search.value, status.value);
        }
    };
    window.filterAppointments = () => {
        const search = document.getElementById('appointmentSearch');
        if (search) {
            filterTable('appointmentsTable', search.value);
        }
    };
    window.filterPrescriptions = () => {
        const search = document.getElementById('prescriptionSearch');
        if (search) {
            filterTable('prescriptionsTable', search.value);
        }
    };
    window.filterLabs = () => {
        const search = document.getElementById('labSearch');
        const status = document.getElementById('labStatus');
        if (search && status) {
            filterTable('labTable', search.value, status.value);
        }
    };
}

function filterTable(tableId, searchQuery, statusFilter = 'All') {
    const table = document.getElementById(tableId);
    if (!table) return;
    const rows = table.querySelectorAll('tbody tr');
    rows.forEach(row => {
        const text = row.textContent.toLowerCase();
        const statusCell = row.querySelector('td:last-child span');
        const statusText = statusCell ? statusCell.textContent : '';
        const matchesSearch = text.includes(searchQuery.toLowerCase());
        const matchesStatus = statusFilter === 'All' || statusText.includes(statusFilter);
        row.style.display = matchesSearch && matchesStatus ? '' : 'none';
    });
}

// Action Functions (Mock)
window.onboardOrg = () => {
    showAlert('Organization onboarded successfully!', 'success');
    const modalElement = document.getElementById('onboardOrgModal');
    if (modalElement && typeof bootstrap !== 'undefined') {
        const modalInstance = bootstrap.Modal.getInstance(modalElement);
        if (modalInstance) modalInstance.hide();
    }
    const form = document.getElementById('onboardForm');
    if (form) form.reset();
    populateOrganizations();
};

window.orderTest = () => {
    showAlert('Test ordered successfully!', 'success');
    const modalElement = document.getElementById('orderTestModal');
    if (modalElement && typeof bootstrap !== 'undefined') {
        const modalInstance = bootstrap.Modal.getInstance(modalElement);
        if (modalInstance) modalInstance.hide();
    }
    const form = document.getElementById('testForm');
    if (form) form.reset();
    populateLaboratory();
};

window.saveAppointment = () => {
    showAlert('Appointment scheduled successfully!', 'success');
    const modalElement = document.getElementById('scheduleAppointmentModal');
    if (modalElement && typeof bootstrap !== 'undefined') {
        const modalInstance = bootstrap.Modal.getInstance(modalElement);
        if (modalInstance) modalInstance.hide();
    }
    const form = document.getElementById('scheduleForm');
    if (form) form.reset();
    populateAppointments();
};

window.savePrescription = () => {
    showAlert('Prescription generated successfully!', 'success');
    const modalElement = document.getElementById('addPrescriptionModal');
    if (modalElement && typeof bootstrap !== 'undefined') {
        const modalInstance = bootstrap.Modal.getInstance(modalElement);
        if (modalInstance) modalInstance.hide();
    }
    const form = document.getElementById('prescriptionForm');
    if (form) form.reset();
    populatePrescriptions();
};

window.saveAmbulanceRequest = () => {
    showAlert('Ambulance request submitted!', 'success');
    const modalElement = document.getElementById('newAmbulanceModal');
    if (modalElement && typeof bootstrap !== 'undefined') {
        const modalInstance = bootstrap.Modal.getInstance(modalElement);
        if (modalInstance) modalInstance.hide();
    }
    const form = document.getElementById('ambulanceForm');
    if (form) form.reset();
    populateAmbulance();
};

window.savePharmacyItem = () => {
    showAlert('Pharmacy item added!', 'success');
    const modalElement = document.getElementById('addPharmacyItemModal');
    if (modalElement && typeof bootstrap !== 'undefined') {
        const modalInstance = bootstrap.Modal.getInstance(modalElement);
        if (modalInstance) modalInstance.hide();
    }
    const form = document.getElementById('pharmacyForm');
    if (form) form.reset();
    populatePharmacy();
};

window.saveBloodEntry = () => {
    showAlert('Blood entry added!', 'success');
    const modalElement = document.getElementById('addBloodEntryModal');
    if (modalElement && typeof bootstrap !== 'undefined') {
        const modalInstance = bootstrap.Modal.getInstance(modalElement);
        if (modalInstance) modalInstance.hide();
    }
    const form = document.getElementById('bloodForm');
    if (form) form.reset();
    populateBloodBank();
};

window.saveInvoice = () => {
    showAlert('Invoice created!', 'success');
    const modalElement = document.getElementById('newInvoiceModal');
    if (modalElement && typeof bootstrap !== 'undefined') {
        const modalInstance = bootstrap.Modal.getInstance(modalElement);
        if (modalInstance) modalInstance.hide();
    }
    const form = document.getElementById('invoiceForm');
    if (form) form.reset();
    populateBilling();
};

window.saveDepartment = () => {
    showAlert('Department added!', 'success');
    const modalElement = document.getElementById('addDepartmentModal');
    if (modalElement && typeof bootstrap !== 'undefined') {
        const modalInstance = bootstrap.Modal.getInstance(modalElement);
        if (modalInstance) modalInstance.hide();
    }
    const form = document.getElementById('departmentForm');
    if (form) form.reset();
    populateDepartments();
};

window.saveInventoryItem = () => {
    showAlert('Inventory item added!', 'success');
    const modalElement = document.getElementById('addInventoryItemModal');
    if (modalElement && typeof bootstrap !== 'undefined') {
        const modalInstance = bootstrap.Modal.getInstance(modalElement);
        if (modalInstance) modalInstance.hide();
    }
    const form = document.getElementById('inventoryForm');
    if (form) form.reset();
    populateInventory();
};

window.saveStaff = () => {
    showAlert('Staff member added!', 'success');
    const modalElement = document.getElementById('addStaffModal');
    if (modalElement && typeof bootstrap !== 'undefined') {
        const modalInstance = bootstrap.Modal.getInstance(modalElement);
        if (modalInstance) modalInstance.hide();
    }
    const form = document.getElementById('staffForm');
    if (form) form.reset();
    populateStaff();
};

window.saveSession = () => {
    showAlert('Session scheduled!', 'success');
    const modalElement = document.getElementById('scheduleSessionModal');
    if (modalElement && typeof bootstrap !== 'undefined') {
        const modalInstance = bootstrap.Modal.getInstance(modalElement);
        if (modalInstance) modalInstance.hide();
    }
    const form = document.getElementById('sessionForm');
    if (form) form.reset();
};

window.uploadDocument = () => {
    showAlert('Document uploaded and OCR processed!', 'success');
    const modalElement = document.getElementById('uploadDocumentModal');
    if (modalElement && typeof bootstrap !== 'undefined') {
        const modalInstance = bootstrap.Modal.getInstance(modalElement);
        if (modalInstance) modalInstance.hide();
    }
    const form = document.getElementById('documentForm');
    if (form) form.reset();
    populateDocuments();
};

window.saveEditProfile = () => {
    showAlert('Profile updated!', 'success');
    const modalElement = document.getElementById('editProfileModal');
    if (modalElement && typeof bootstrap !== 'undefined') {
        const modalInstance = bootstrap.Modal.getInstance(modalElement);
        if (modalInstance) modalInstance.hide();
    }
};

window.onboardClient = () => {
    showAlert('New client onboarding started!', 'info');
};

window.runSymptomChecker = () => {
    const input = document.getElementById('symptomInput');
    const results = document.getElementById('triageResults');
    if (input && results) {
        results.innerHTML = input.value ? `<p class="text-info">Based on symptoms: ${input.value}, recommended: Consult doctor for chest pain.</p>` : '<p class="text-warning">Please describe symptoms.</p>';
    }
};

window.recommendTests = () => {
    const patient = document.getElementById('patientForRec');
    const recs = document.getElementById('testRecs');
    if (patient && recs) {
        recs.innerHTML = patient.value ? '<li>Blood Glucose Test</li><li>Cholesterol Panel</li>' : '<li>Select a patient first.</li>';
    }
};

window.syncFitnessData = () => {
    showAlert('Fitness data synced successfully!', 'success');
};

window.generateReport = () => {
    const type = document.getElementById('reportType');
    const start = document.getElementById('reportStart');
    const end = document.getElementById('reportEnd');
    if (type && start && end) {
        showAlert(`Report "${type.value}" generated for ${start.value} to ${end.value}!`, 'success');
    }
};

window.manageIntegrations = () => {
    showAlert('Integration management opened!', 'info');
};

window.setupNotifications = () => {
    showAlert('Notification setup configured!', 'success');
};

// Utility Functions
function showAlert(message, type = 'info') {
    alert(`${type.toUpperCase()}: ${message}`);
}

function handleWindowResize() {
    if (window.innerWidth > 768) {
        const sidebar = document.getElementById('sidebar');
        const mainContent = document.getElementById('mainContent');
        if (sidebar) sidebar.classList.remove('show');
        if (mainContent) mainContent.classList.remove('expanded');
    }
}

function handleKeyboardShortcuts(e) {
    if (e.ctrlKey || e.metaKey) {
        switch (e.key) {
            case 'k':
                e.preventDefault();
                const search = document.getElementById('globalSearch');
                if (search) search.focus();
                break;
            case 'n':
                e.preventDefault();
                const modalElement = document.getElementById('addPatientModal');
                if (modalElement && typeof bootstrap !== 'undefined') {
                    new bootstrap.Modal(modalElement).show();
                }
                break;
        }
    }
}

// AI Mock Functions
window.inithealthassistant = () => {
    const tips = document.getElementById('wellnessTips');
    if (tips) {
        tips.innerHTML = '<p>Stay hydrated and get 8 hours of sleep!</p>';
    }
    const select = document.getElementById('patientForRec');
    if (select) {
        select.innerHTML = '<option>John Smith</option><option>Emily Davis</option>';
    }
};

// Prescription Canvas Initialization
function initializePrescriptionCanvas() {
    const canvas = document.getElementById('prescriptionCanvas');
    if (!canvas) return;

    const ctx = canvas.getContext('2d');

    // Inputs
    const doctorName = document.getElementById('doctorName');
    const specialization = document.getElementById('specialization');
    const regNo = document.getElementById('regNo');
    const contact = document.getElementById('contact');
    const patientName = document.getElementById('patientName');
    const patientAge = document.getElementById('patientAge');
    const patientGender = document.getElementById('patientGender');
    const prescriptionDate = document.getElementById('prescriptionDate');
    const colorPicker = document.getElementById('colorPicker');
    const brushSize = document.getElementById('brushSize');
    const sizeValue = document.getElementById('sizeValue');
    const textInput = document.getElementById('textInput');
    const fontSize = document.getElementById('fontSize');
    const fontFamily = document.getElementById('fontFamily');
    const gridOverlay = document.getElementById('gridOverlay');
    const gridTool = document.getElementById('gridTool');
    const historyPanel = document.getElementById('historyPanel');
    const historyList = document.getElementById('historyList');
    const textTools = document.getElementById('textTools');
    const medicineSelect = document.getElementById('medicineSelect');

    if (!ctx) return;

    // Set canvas size
    function resizeCanvas() {
        const currentData = canvas.toDataURL();
        const container = canvas.parentElement;
        canvas.width = container.offsetWidth || 1200;
        canvas.height = 1600;

        const img = new Image();
        img.src = currentData;
        img.onload = () => {
            ctx.drawImage(img, 0, 0, canvas.width, canvas.height);
        };

        ctx.lineCap = 'round';
        ctx.lineJoin = 'round';
    }

    resizeCanvas();
    window.addEventListener('resize', resizeCanvas);

    // Drawing state
    let isDrawing = false;
    let currentTool = 'pen';
    let currentColor = '#000000';
    let currentSize = 1;
    let history = [];
    let historyStep = -1;
    let currentPath = [];
    let currentY = 0;
    let medicineYs = [];
    let medicineLine = 1;

    // Tools
    const tools = {
        pen: { globalCompositeOperation: 'source-over', opacity: 1 },
        highlighter: { globalCompositeOperation: 'multiply', opacity: 0.3 },
        eraser: { globalCompositeOperation: 'destination-out', opacity: 1 }
    };

    // Event listeners for drawing
    canvas.addEventListener('mousedown', startDrawing);
    canvas.addEventListener('mousemove', draw);
    canvas.addEventListener('mouseup', stopDrawing);
    canvas.addEventListener('mouseout', stopDrawing);
    canvas.addEventListener('touchstart', handleTouch);
    canvas.addEventListener('touchmove', handleTouch);
    canvas.addEventListener('touchend', stopDrawing);

    function handleTouch(e) {
        e.preventDefault();
        const touch = e.touches[0];
        const mouseEvent = new MouseEvent(e.type === 'touchstart' ? 'mousedown' : 'mousemove', {
            clientX: touch.clientX,
            clientY: touch.clientY
        });
        if (e.type === 'touchstart') {
            startDrawing(mouseEvent);
        } else if (e.type === 'touchmove') {
            draw(mouseEvent);
        }
    }

    function startDrawing(e) {
        if (currentTool === 'text') return;
        isDrawing = true;
        const rect = canvas.getBoundingClientRect();
        const x = e.clientX - rect.left;
        const y = e.clientY - rect.top;
        currentPath = [{ x, y }];
        ctx.beginPath();
        ctx.moveTo(x, y);
        ctx.globalCompositeOperation = tools[currentTool].globalCompositeOperation;
        ctx.globalAlpha = tools[currentTool].opacity;
        ctx.strokeStyle = currentTool === 'eraser' ? '#FFFFFF' : currentColor;
        ctx.lineWidth = currentSize;
    }

    function draw(e) {
        if (!isDrawing || currentTool === 'text') return;
        const rect = canvas.getBoundingClientRect();
        const x = e.clientX - rect.left;
        const y = e.clientY - rect.top;
        currentPath.push({ x, y });
        ctx.lineTo(x, y);
        ctx.stroke();
    }

    function stopDrawing() {
        if (!isDrawing) return;
        isDrawing = false;
        if (currentPath.length > 0) {
            saveHistory();
        }
        ctx.globalAlpha = 1;
    }

    window.selectTool = function (tool) {
        currentTool = tool;
        document.querySelectorAll('.tool-btn').forEach(btn => btn.classList.remove('active'));
        const toolBtn = document.getElementById(tool + 'Tool');
        if (toolBtn) toolBtn.classList.add('active');
        if (textTools) {
            textTools.style.display = tool === 'text' ? 'flex' : 'none';
        }
        if (tool === 'eraser') {
            canvas.style.cursor = 'grab';
        } else if (tool === 'text') {
            canvas.style.cursor = 'text';
        } else {
            canvas.style.cursor = 'crosshair';
        }
    };

    // Color and size controls
    if (colorPicker) {
        colorPicker.addEventListener('change', (e) => {
            currentColor = e.target.value;
        });
    }

    if (brushSize && sizeValue) {
        brushSize.addEventListener('input', (e) => {
            currentSize = e.target.value;
            sizeValue.textContent = e.target.value;
        });
    }

    // History management
    function saveHistory() {
        historyStep++;
        if (historyStep < history.length) {
            history = history.slice(0, historyStep);
        }
        history.push(canvas.toDataURL());
        if (history.length > 50) {
            history.shift();
            historyStep--;
        }
        updateHistoryList();
    }

    function updateHistoryList() {
        if (!historyList) return;
        historyList.innerHTML = '';
        history.forEach((src, index) => {
            const img = document.createElement('img');
            img.src = src;
            img.width = 100;
            img.onclick = () => {
                historyStep = index;
                loadFromHistory();
            };
            historyList.appendChild(img);
        });
    }

    function loadFromHistory() {
        const img = new Image();
        img.src = history[historyStep];
        img.onload = () => {
            ctx.clearRect(0, 0, canvas.width, canvas.height);
            ctx.drawImage(img, 0, 0, canvas.width, canvas.height);
        };
    }

    window.undo = function () {
        if (historyStep > 0) {
            historyStep--;
            loadFromHistory();
        } else if (historyStep === 0) {
            ctx.clearRect(0, 0, canvas.width, canvas.height);
            historyStep = -1;
        }
    };

    window.redo = function () {
        if (historyStep < history.length - 1) {
            historyStep++;
            loadFromHistory();
        }
    };

    window.clearCanvas = function () {
        if (confirm('Clear the entire prescription? This cannot be undone.')) {
            ctx.clearRect(0, 0, canvas.width, canvas.height);
            history = [];
            historyStep = -1;
            currentY = 0;
            medicineYs = [];
            medicineLine = 1;
            updateHistoryList();
        }
    };

    // Text functionality
    window.addText = function () {
        if (!textInput) return;
        const text = textInput.value;
        if (!text) return;
        const fSize = fontSize ? fontSize.value : '14';
        const fFamily = fontFamily ? fontFamily.value : 'Arial';
        canvas.addEventListener('click', placeText, { once: true });
        function placeText(e) {
            const rect = canvas.getBoundingClientRect();
            const x = e.clientX - rect.left;
            const y = e.clientY - rect.top;
            ctx.globalCompositeOperation = 'source-over';
            ctx.font = `${fSize}px ${fFamily}`;
            ctx.fillStyle = currentColor;
            ctx.fillText(text, x, y);
            textInput.value = '';
            saveHistory();
        }
    };

    // Templates
    window.addTemplate = function (type) {
        if (currentY === 0) {
            const header = document.querySelector('.prescription-header');
            currentY = (header ? header.offsetHeight : 0) + 50;
        }
        ctx.globalCompositeOperation = 'source-over';
        ctx.fillStyle = '#000000';
        switch (type) {
            case 'Rx':
                ctx.font = 'bold 36px serif';
                ctx.fillText('℞', 50, currentY);
                currentY += 50;
                break;
            case 'diagnosis':
                ctx.font = 'bold 16px Arial';
                ctx.fillText('Diagnosis:', 50, currentY);
                ctx.font = '14px Arial';
                ctx.fillText('_________________________________', 150, currentY);
                currentY += 40;
                break;
            case 'medicines':
                ctx.font = 'bold 16px Arial';
                ctx.fillText('Medicines:', 50, currentY);
                currentY += 30;
                ctx.font = '14px Arial';
                medicineYs = [];
                for (let i = 1; i <= 5; i++) {
                    const y = currentY;
                    medicineYs.push(y);
                    ctx.fillText(`${i}.`, 50, y);
                    currentY += 30;
                }
                break;
            case 'dosage':
                drawDosageTable(currentY);
                currentY += 180;
                break;
            case 'advice':
                ctx.font = 'bold 16px Arial';
                ctx.fillText('Advice:', 50, currentY);
                currentY += 30;
                ctx.font = '14px Arial';
                ctx.fillText('_________________________________', 50, currentY);
                currentY += 30;
                ctx.fillText('_________________________________', 50, currentY);
                currentY += 30;
                break;
            case 'followup':
                ctx.font = 'bold 16px Arial';
                ctx.fillText('Next Visit:', 50, currentY);
                ctx.font = '14px Arial';
                ctx.fillText('_________________________________', 150, currentY);
                currentY += 40;
                break;
        }
        saveHistory();
    };

    function drawDosageTable(startY) {
        const startX = 50;
        const cellWidth = [200, 70, 70, 70];
        const cellHeight = 30;
        const rows = 6;
        ctx.strokeStyle = '#000000';
        ctx.lineWidth = 1;
        // Horizontal lines
        for (let i = 0; i <= rows; i++) {
            ctx.beginPath();
            ctx.moveTo(startX, startY + i * cellHeight);
            ctx.lineTo(startX + cellWidth.reduce((a, b) => a + b, 0), startY + i * cellHeight);
            ctx.stroke();
        }
        // Vertical lines
        let x = startX;
        for (let w of cellWidth) {
            ctx.beginPath();
            ctx.moveTo(x, startY);
            ctx.lineTo(x, startY + rows * cellHeight);
            ctx.stroke();
            x += w;
        }
        // Headers
        ctx.font = 'bold 14px Arial';
        ctx.fillStyle = '#000000';
        const headers = ['Medicine', 'Morning', 'Afternoon', 'Night'];
        x = startX;
        for (let j = 0; j < headers.length; j++) {
            ctx.fillText(headers[j], x + 10, startY + 20);
            x += cellWidth[j];
        }
        // Checkboxes
        for (let row = 1; row < rows; row++) {
            let checkboxX = startX + cellWidth[0];
            for (let col = 0; col < 3; col++) {
                ctx.beginRect();
                ctx.rect(checkboxX + (cellWidth[col + 1] - 20) / 2, startY + row * cellHeight + (cellHeight - 20) / 2, 20, 20);
                ctx.stroke();
                checkboxX += cellWidth[col + 1];
            }
        }
    }

    window.addMedicine = function () {
        if (!medicineSelect) return;
        const med = medicineSelect.value;
        if (!med) return;
        if (medicineYs.length === 0) {
            alert('Please add the Medicines template first.');
            return;
        }
        const nextIndex = medicineLine - 1;
        if (nextIndex >= medicineYs.length) {
            alert('Maximum of 5 medicines reached.');
            return;
        }
        ctx.globalCompositeOperation = 'source-over';
        ctx.font = '14px Arial';
        ctx.fillStyle = '#000000';
        ctx.fillText(med, 70, medicineYs[nextIndex]);
        medicineLine++;
        saveHistory();
        medicineSelect.value = '';
    };

    // Update header
    function updateHeader() {
        const headerContent = document.getElementById('headerContent');
        if (!headerContent) return;
        const gender = patientGender ? (patientGender.value ? patientGender.value : '') : '';
        const age = patientAge ? (patientAge.value ? `${patientAge.value} years` : '') : '';
        const date = prescriptionDate ? (prescriptionDate.value ? new Date(prescriptionDate.value).toLocaleDateString() : '') : '';
        headerContent.innerHTML = `
            <h2>Doctor: ${doctorName ? doctorName.value || 'Doctor Name' : 'Doctor Name'} - Specialization: ${specialization ? specialization.value || 'Specialization' : 'Specialization'}</h2>
            <p>Reg No: ${regNo ? regNo.value || '' : ''} | Contact: ${contact ? contact.value || '' : ''}</p>
            <hr>
            <p>Patient: ${patientName ? patientName.value || '' : ''}, ${age} ${gender}, Date: ${date}</p>
        `;
    }

    // Toggle functions
    window.toggleGrid = function () {
        if (gridOverlay) gridOverlay.classList.toggle('show');
        if (gridTool) gridTool.classList.toggle('active');
    };

    window.toggleHistory = function () {
        if (historyPanel) historyPanel.classList.toggle('show');
    };

    // Save functions
    window.saveAsPDF = function () {
        const container = document.querySelector('.canvas-container');
        if (!container || typeof html2canvas === 'undefined') {
            showAlert('PDF export not available', 'error');
            return;
        }
        html2canvas(container, { scale: 2 }).then(c => {
            const imgData = c.toDataURL('image/png');
            if (typeof jsPDF === 'undefined') {
                showAlert('PDF library not loaded', 'error');
                return;
            }
            const doc = new jsPDF('p', 'mm', 'a4');
            const pdfWidth = doc.internal.pageSize.getWidth();
            const pdfHeight = doc.internal.pageSize.getHeight();
            const imgWidth = c.width / 2;
            const imgHeight = c.height / 2;
            const ratio = Math.min(pdfWidth / imgWidth, pdfHeight / imgHeight);
            doc.addImage(imgData, 'PNG', 0, 0, imgWidth * ratio, imgHeight * ratio);
            doc.save('prescription.pdf');
        });
    };

    window.saveAsImage = function () {
        const container = document.querySelector('.canvas-container');
        if (!container || typeof html2canvas === 'undefined') {
            showAlert('Image export not available', 'error');
            return;
        }
        html2canvas(container).then(c => {
            const link = document.createElement('a');
            link.download = 'prescription.png';
            link.href = c.toDataURL('image/png');
            link.click();
        });
    };

    window.printPrescription = function () {
        window.print();
    };

    window.newPrescription = function () {
        if (confirm('Start a new prescription? This will clear the current one.')) {
            ctx.clearRect(0, 0, canvas.width, canvas.height);
            history = [];
            historyStep = -1;
            currentY = 0;
            medicineYs = [];
            medicineLine = 1;
            updateHistoryList();
            if (patientName) patientName.value = '';
            if (patientAge) patientAge.value = '';
            if (patientGender) patientGender.value = '';
            if (prescriptionDate) prescriptionDate.value = new Date().toISOString().slice(0, 10);
            updateHeader();
        }
    };

    // Keyboard shortcuts for prescription
    document.addEventListener('keydown', (e) => {
        if (e.ctrlKey) {
            if (e.key === 'z') {
                e.preventDefault();
                window.undo();
            }
            if (e.key === 'y') {
                e.preventDefault();
                window.redo();
            }
            if (e.key === 's') {
                e.preventDefault();
                window.saveAsPDF();
            }
            if (e.key === 'p') {
                e.preventDefault();
                window.printPrescription();
            }
        }
        if (e.key === 'Delete') {
            window.clearCanvas();
        }
        if (e.key.toLowerCase() === 'g') {
            window.toggleGrid();
        }
        if (e.key >= '1' && e.key <= '5') {
            const size = parseInt(e.key) * 2;
            if (brushSize) brushSize.value = size;
            currentSize = size;
            if (sizeValue) sizeValue.textContent = size;
        }
    });

    // Initialize
    const inputs = [doctorName, specialization, regNo, contact, patientName, patientAge, patientGender, prescriptionDate];
    inputs.forEach(input => {
        if (input) input.addEventListener('input', updateHeader);
    });
    if (prescriptionDate) prescriptionDate.value = new Date().toISOString().slice(0, 10);
    updateHeader();
    saveHistory(); // Initial blank state

    // Fix for modal: Resize canvas when modal is shown
    const prescriptionModal = document.getElementById('addPrescriptionModal');
    if (prescriptionModal) {
        prescriptionModal.addEventListener('shown.bs.modal', resizeCanvas);
    }
}
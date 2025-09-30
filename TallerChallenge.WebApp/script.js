document.addEventListener('DOMContentLoaded', () => {
    const productTableBody = document.getElementById('product-table-body');
    const errorMessageDiv = document.getElementById('error-message');
    const filterButton = document.getElementById('filter-button');
    const nameFilter = document.getElementById('name-filter');
    const descriptionFilter = document.getElementById('description-filter');
    const priceFromFilter = document.getElementById('price-from-filter');
    const priceToFilter = document.getElementById('price-to-filter');

    const { apiKey, apiUrl } = AppConfig; 

    async function fetchProducts(filters = {}) {
        try {
            const params = new URLSearchParams();
            if (filters.name) params.append('name', filters.name);
            if (filters.description) params.append('description', filters.description);
            if (filters.priceFrom) params.append('priceFrom', filters.priceFrom);
            if (filters.priceTo) params.append('priceTo', filters.priceTo);

            const queryString = params.toString();
            const url = queryString ? `${apiUrl}?${queryString}` : apiUrl;

            const response = await fetch(url, {
                headers: {
                    'X-API-KEY': apiKey
                }
            });

            if (response.status === 401) {
                throw new Error('Unauthorized: Invalid or missing API Key.');
            }
            if (!response.ok) {
                throw new Error(`API request failed with status: ${response.status}`);
            }

            const products = await response.json();
            displayProducts(products);

        } catch (error) {
            showError(error.message);
        }
    }

    function displayProducts(products) {
        productTableBody.innerHTML = ''; // Clear existing rows
        if (products.length === 0) {
            const row = productTableBody.insertRow();
            const cell = row.insertCell();
            cell.colSpan = 4;
            cell.textContent = 'No products found.';
            return;
        }

        products.forEach(product => {
            const row = productTableBody.insertRow();
            row.insertCell().textContent = product.id;
            row.insertCell().textContent = product.name;
            row.insertCell().textContent = product.description;
            row.insertCell().textContent = product.price.toFixed(2);
        });
    }

    function showError(message) {
        errorMessageDiv.textContent = message;
        errorMessageDiv.style.display = 'block';
    }

    filterButton.addEventListener('click', () => {
        const filters = {
            name: nameFilter.value,
            description: descriptionFilter.value,
            priceFrom: priceFromFilter.value,
            priceTo: priceToFilter.value
        };
        fetchProducts(filters);
    });

    fetchProducts();
});

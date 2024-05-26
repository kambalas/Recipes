window.provideSortDropdownListeners = function() {
    const dropdownButton = document.querySelector('.dropdown-button.sort-button');
    const dropdownMenu = document.querySelector('.dropdown-menu');

    dropdownButton.addEventListener('click', function () {
        if (dropdownMenu.style.display === 'block') {
            dropdownMenu.style.display = 'none';
        } else {
            dropdownMenu.style.display = 'block';

        }
    });

    document.querySelectorAll('.dropdown-item').forEach(item => {
        item.addEventListener('click', function () {
            const value = this.getAttribute('data-value');
            dropdownMenu.style.display = 'none';
        });
    });

    document.addEventListener('click', function (event) {
        if (!event.target.closest('.dropdown-container')) {
            dropdownMenu.style.display = 'none';
        }
    });
    
    
};



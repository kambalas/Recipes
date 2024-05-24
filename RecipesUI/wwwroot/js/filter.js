window.provideSortDropdownListeners = function() {
    const dropdownButton = document.querySelector('.dropdown-button.sort-button');
    const dropdownMenu = document.querySelector('.dropdown-menu');
    const arrowIcon = document.querySelector('.arrow-icon');

    dropdownButton.addEventListener('click', function () {
        if (dropdownMenu.style.display === 'block') {
            dropdownMenu.style.display = 'none';
            // arrowIcon.style.transform = 'translateY(-50%) rotate(0)';
        } else {
            dropdownMenu.style.display = 'block';
            // arrowIcon.style.transform = 'translateY(-50%) rotate(180deg)';
        }
    });

    document.querySelectorAll('.dropdown-item').forEach(item => {
        item.addEventListener('click', function () {
            const value = this.getAttribute('data-value');
            sortRecipes(value);
            dropdownMenu.style.display = 'none';
            // arrowIcon.style.transform = 'translateY(-50%) rotate(0)';
        });
    });

    document.addEventListener('click', function (event) {
        if (!event.target.closest('.dropdown-container')) {
            dropdownMenu.style.display = 'none';
            // -arrowIcon.style.transform = 'translateY(-50%) rotate(0)';
        }
    });
};


// window.sortRecipes = {
//
//
//     sortRecipes : function (order) {
//         console.log('Sorting recipes by:', order);
//         DotNet.invokeMethodAsync('Recipes', 'SortRecipes', order) 
//
//         //HERE WE WILL NEED TO DECIDE ON A CONCRETE PROJECT NAME IN ORDER TO CALL THE JS METHOD
//
//    
//         .then(() => {
//             console.log('Successfully sorted recipes by:', order);
//         })
//             .catch(error => {
//                 console.error('Error sorting recipes:', error);
//             });
//
//     }

// };

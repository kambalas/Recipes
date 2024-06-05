function triggerClickOnFileInput(fileInputId) {
    document.getElementById(fileInputId).click();
}


// Ingredients
window.autoCompleteInstance = null;

window.provideAutoComplete = function() {
    if (!window.autoCompleteInstance) {
        window.autoCompleteInstance = new autoComplete({
            selector: "#autoComplete",
            placeHolder: "Search for Ingredients...",
            data: {
                src: [],
                keys: ["name"],
                cache: false
            },
            resultItem: {
                highlight: true
            },
            events: {
                input: {
                    selection: event => {
                        const feedback = event.detail;
                        const autoCompleteElement = document.getElementById("autoComplete");
                        autoCompleteElement.value = feedback.selection.value.name;
                        autoCompleteElement.dataset.id = feedback.selection.value.id;
                    }
                }
            }
        });
    }
};

window.updateAutoCompleteData = function(ingredientsData) {
    if (window.autoCompleteInstance) {
        window.autoCompleteInstance.data.src = ingredientsData;
    }
};


function loadIngredients(ingredients) {
    
    const list = document.getElementById('ingredients-list');
    list.innerHTML = '';
    ingredients.forEach(ingredient => {
        const item = document.createElement('div');
        item.className = 'ingredient-item';
        item.dataset.id = ingredient.id;
        item.dataset.name = ingredient.name;
        item.dataset.amount = ingredient.amount;
        item.dataset.measurement = ingredient.measurement;
        item.innerHTML = `<div class="wrapper">
                          <svg width="40" height="40" viewBox="0 0 48 48" fill="none" xmlns="http://www.w3.org/2000/svg"><path fill-rule="evenodd" clip-rule="evenodd" d="M17.88 15.76C18.6438 15.76 19.3764 15.4566 19.9165 14.9165C20.4566 14.3764 20.76 13.6438 20.76 12.88C20.76 12.1162 20.4566 11.3836 19.9165 10.8435C19.3764 10.3034 18.6438 10 17.88 10C17.1162 10 16.3836 10.3034 15.8435 10.8435C15.3034 11.3836 15 12.1162 15 12.88C15 13.6438 15.3034 14.3764 15.8435 14.9165C16.3836 15.4566 17.1162 15.76 17.88 15.76ZM17.88 27.28C18.2582 27.28 18.6327 27.2055 18.9821 27.0608C19.3315 26.916 19.649 26.7039 19.9165 26.4365C20.1839 26.169 20.396 25.8515 20.5408 25.5021C20.6855 25.1527 20.76 24.7782 20.76 24.4C20.76 24.0218 20.6855 23.6473 20.5408 23.2979C20.396 22.9485 20.1839 22.631 19.9165 22.3635C19.649 22.0961 19.3315 21.884 18.9821 21.7392C18.6327 21.5945 18.2582 21.52 17.88 21.52C17.1162 21.52 16.3836 21.8234 15.8435 22.3635C15.3034 22.9036 15 23.6362 15 24.4C15 25.1638 15.3034 25.8964 15.8435 26.4365C16.3836 26.9766 17.1162 27.28 17.88 27.28ZM20.76 35.92C20.76 36.6838 20.4566 37.4164 19.9165 37.9565C19.3764 38.4966 18.6438 38.8 17.88 38.8C17.1162 38.8 16.3836 38.4966 15.8435 37.9565C15.3034 37.4164 15 36.6838 15 35.92C15 35.1562 15.3034 34.4236 15.8435 33.8835C16.3836 33.3434 17.1162 33.04 17.88 33.04C18.6438 33.04 19.3764 33.3434 19.9165 33.8835C20.4566 34.4236 20.76 35.1562 20.76 35.92ZM29.4 15.76C30.1638 15.76 30.8964 15.4566 31.4365 14.9165C31.9766 14.3764 32.28 13.6438 32.28 12.88C32.28 12.1162 31.9766 11.3836 31.4365 10.8435C30.8964 10.3034 30.1638 10 29.4 10C28.6362 10 27.9036 10.3034 27.3635 10.8435C26.8234 11.3836 26.52 12.1162 26.52 12.88C26.52 13.6438 26.8234 14.3764 27.3635 14.9165C27.9036 15.4566 28.6362 15.76 29.4 15.76ZM32.28 24.4C32.28 25.1638 31.9766 25.8964 31.4365 26.4365C30.8964 26.9766 30.1638 27.28 29.4 27.28C28.6362 27.28 27.9036 26.9766 27.3635 26.4365C26.8234 25.8964 26.52 25.1638 26.52 24.4C26.52 23.6362 26.8234 22.9036 27.3635 22.3635C27.9036 21.8234 28.6362 21.52 29.4 21.52C30.1638 21.52 30.8964 21.8234 31.4365 22.3635C31.9766 22.9036 32.28 23.6362 32.28 24.4ZM29.4 38.8C30.1638 38.8 30.8964 38.4966 31.4365 37.9565C31.9766 37.4164 32.28 36.6838 32.28 35.92C32.28 35.1562 31.9766 34.4236 31.4365 33.8835C30.8964 33.3434 30.1638 33.04 29.4 33.04C28.6362 33.04 27.9036 33.3434 27.3635 33.8835C26.8234 34.4236 26.52 35.1562 26.52 35.92C26.52 36.6838 26.8234 37.4164 27.3635 37.9565C27.9036 38.4966 28.6362 38.8 29.4 38.8Z" fill="#AAAAAA"/></svg>
                          <div class="name">${ingredient.name}: ${ingredient.amount} ${ingredient.measurement}</div>
                          <div class="spacer"></div>
                          <div class="ingredient-buttons">
                              <button class="edit-button">
                                  <svg width="26" height="23" viewBox="0 0 26 23" fill="none" xmlns="http://www.w3.org/2000/svg"><path d="M24.52 21.9999H1" stroke="#333" stroke-width="1.5" stroke-linecap="round"/><path d="M6.84645 13.7344L6.04004 16.96L9.26568 16.1536C9.3536 16.1316 9.43389 16.0862 9.49797 16.0221L21.6465 3.87355C21.8417 3.67829 21.8417 3.36171 21.6465 3.16645L19.8336 1.35355C19.6383 1.15829 19.3217 1.15829 19.1265 1.35355L6.97797 13.5021C6.91389 13.5662 6.86843 13.6464 6.84645 13.7344Z" stroke="#333" stroke-width="1.5" stroke-linejoin="round"/></svg>
                              </button>
                              <button class="delete-button">
                                  <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><path d="M6.09375 16.75L4.875 7L4.5 4H19.5L19 7L17.375 16.75M6.09375 16.75L6.69525 21.562C6.72653 21.8122 6.93923 22 7.19139 22H16.0764C16.3209 22 16.5295 21.8233 16.5696 21.5822L17.375 16.75M6.09375 16.75H17.375" stroke="#333" stroke-width="1.5"/><rect x="3" y="1" width="18" height="3" rx="1.5" stroke="#333" stroke-width="1.5"/></svg>
                                  </button>
                          </div>
                      </div>`;
        item.querySelector('.edit-button').addEventListener('click', () => window.editIngredient(item));
        item.querySelector('.delete-button').addEventListener('click', () => window.deleteIngredient(item));
        list.appendChild(item);
    });
}

function ingredientsFunctions(dotnetHelper) {

    window.addIngredient = function () {
        const input = document.getElementById('autoComplete');
        const name = input.value.trim();
        const ingredientId = input.dataset.id;
        const amount = document.getElementById('ingredient-amount').value;
        const measurement = document.getElementById('ingredient-measurement').value;

       // Error handling
        // TODO: Add UI error handling instead of alerts (then clean up return statements on each error check)
       
       if (!name || !ingredientId || !amount || !measurement || isNaN(amount) || amount <= 0 || document.querySelector(`.ingredient-item[data-id="${ingredientId}"]`)) {
           if (!name) {
               alert('Ingredient must be selected from the list! Use the search bar to find the ingredient.');
               return;
           }
           
           if (!ingredientId) {
               alert('Ingredient must be selected from the list!');
               return;
               
           }
           if (!amount) {
               alert('Amount must be filled!');
               return;

           }
           if (!measurement) {
               alert('Measurement must be selected!');
               return;

           }
           if (isNaN(amount)) {
               alert('Amount must be a number!');
               return;

           }
           if (!isNaN(amount) && amount <= 0) {
               alert('Amount must be greater than 0!');
               return;

           }
            if (document.querySelector(`.ingredient-item[data-id="${ingredientId}"]`)) {
               alert('Ingredient already exists! Edit it if you want to change the amount or measurement.');
               return;

            }
           return;
       }
       
        
        const list = document.getElementById('ingredients-list');
        const item = document.createElement('div');
        item.className = 'ingredient-item';
        item.dataset.id = ingredientId;
        item.dataset.name = name;
        item.dataset.amount = amount;
        item.dataset.measurement = measurement;
        item.innerHTML = `
                      <div class="wrapper">
                          <svg width="40" height="40" viewBox="0 0 48 48" fill="none" xmlns="http://www.w3.org/2000/svg"><path fill-rule="evenodd" clip-rule="evenodd" d="M17.88 15.76C18.6438 15.76 19.3764 15.4566 19.9165 14.9165C20.4566 14.3764 20.76 13.6438 20.76 12.88C20.76 12.1162 20.4566 11.3836 19.9165 10.8435C19.3764 10.3034 18.6438 10 17.88 10C17.1162 10 16.3836 10.3034 15.8435 10.8435C15.3034 11.3836 15 12.1162 15 12.88C15 13.6438 15.3034 14.3764 15.8435 14.9165C16.3836 15.4566 17.1162 15.76 17.88 15.76ZM17.88 27.28C18.2582 27.28 18.6327 27.2055 18.9821 27.0608C19.3315 26.916 19.649 26.7039 19.9165 26.4365C20.1839 26.169 20.396 25.8515 20.5408 25.5021C20.6855 25.1527 20.76 24.7782 20.76 24.4C20.76 24.0218 20.6855 23.6473 20.5408 23.2979C20.396 22.9485 20.1839 22.631 19.9165 22.3635C19.649 22.0961 19.3315 21.884 18.9821 21.7392C18.6327 21.5945 18.2582 21.52 17.88 21.52C17.1162 21.52 16.3836 21.8234 15.8435 22.3635C15.3034 22.9036 15 23.6362 15 24.4C15 25.1638 15.3034 25.8964 15.8435 26.4365C16.3836 26.9766 17.1162 27.28 17.88 27.28ZM20.76 35.92C20.76 36.6838 20.4566 37.4164 19.9165 37.9565C19.3764 38.4966 18.6438 38.8 17.88 38.8C17.1162 38.8 16.3836 38.4966 15.8435 37.9565C15.3034 37.4164 15 36.6838 15 35.92C15 35.1562 15.3034 34.4236 15.8435 33.8835C16.3836 33.3434 17.1162 33.04 17.88 33.04C18.6438 33.04 19.3764 33.3434 19.9165 33.8835C20.4566 34.4236 20.76 35.1562 20.76 35.92ZM29.4 15.76C30.1638 15.76 30.8964 15.4566 31.4365 14.9165C31.9766 14.3764 32.28 13.6438 32.28 12.88C32.28 12.1162 31.9766 11.3836 31.4365 10.8435C30.8964 10.3034 30.1638 10 29.4 10C28.6362 10 27.9036 10.3034 27.3635 10.8435C26.8234 11.3836 26.52 12.1162 26.52 12.88C26.52 13.6438 26.8234 14.3764 27.3635 14.9165C27.9036 15.4566 28.6362 15.76 29.4 15.76ZM32.28 24.4C32.28 25.1638 31.9766 25.8964 31.4365 26.4365C30.8964 26.9766 30.1638 27.28 29.4 27.28C28.6362 27.28 27.9036 26.9766 27.3635 26.4365C26.8234 25.8964 26.52 25.1638 26.52 24.4C26.52 23.6362 26.8234 22.9036 27.3635 22.3635C27.9036 21.8234 28.6362 21.52 29.4 21.52C30.1638 21.52 30.8964 21.8234 31.4365 22.3635C31.9766 22.9036 32.28 23.6362 32.28 24.4ZM29.4 38.8C30.1638 38.8 30.8964 38.4966 31.4365 37.9565C31.9766 37.4164 32.28 36.6838 32.28 35.92C32.28 35.1562 31.9766 34.4236 31.4365 33.8835C30.8964 33.3434 30.1638 33.04 29.4 33.04C28.6362 33.04 27.9036 33.3434 27.3635 33.8835C26.8234 34.4236 26.52 35.1562 26.52 35.92C26.52 36.6838 26.8234 37.4164 27.3635 37.9565C27.9036 38.4966 28.6362 38.8 29.4 38.8Z" fill="#AAAAAA"/></svg>
                          <div class="name">${name}: ${amount} ${measurement}</div>
                          <div class="spacer"></div>
                          <div class="ingredient-buttons">
                              <button class="edit-button">
                                  <svg width="26" height="23" viewBox="0 0 26 23" fill="none" xmlns="http://www.w3.org/2000/svg"><path d="M24.52 21.9999H1" stroke="#333" stroke-width="1.5" stroke-linecap="round"/><path d="M6.84645 13.7344L6.04004 16.96L9.26568 16.1536C9.3536 16.1316 9.43389 16.0862 9.49797 16.0221L21.6465 3.87355C21.8417 3.67829 21.8417 3.36171 21.6465 3.16645L19.8336 1.35355C19.6383 1.15829 19.3217 1.15829 19.1265 1.35355L6.97797 13.5021C6.91389 13.5662 6.86843 13.6464 6.84645 13.7344Z" stroke="#333" stroke-width="1.5" stroke-linejoin="round"/></svg>
                              </button>
                              <button class="delete-button">
                                  <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><path d="M6.09375 16.75L4.875 7L4.5 4H19.5L19 7L17.375 16.75M6.09375 16.75L6.69525 21.562C6.72653 21.8122 6.93923 22 7.19139 22H16.0764C16.3209 22 16.5295 21.8233 16.5696 21.5822L17.375 16.75M6.09375 16.75H17.375" stroke="#333" stroke-width="1.5"/><rect x="3" y="1" width="18" height="3" rx="1.5" stroke="#333" stroke-width="1.5"/></svg>
                                  </button>
                          </div>
                      </div>`;

        item.querySelector('.edit-button').addEventListener('click', () => window.editIngredient(item));
        item.querySelector('.delete-button').addEventListener('click', () => window.deleteIngredient(item));


        list.appendChild(item);

        document.getElementById('autoComplete').value = '';
        document.getElementById('autoComplete').dataset.id = '';
        document.getElementById('ingredient-amount').value = '';

        document.getElementById('ingredient-measurement').value = '';

        const dropdownButton = document.querySelector('#measurement-dropdown .dropdown-button');

        dropdownButton.innerHTML = '<div class="label">Measurement</div><span class="arrow-icon"><svg viewBox="0 0 32 32" xmlns="http://www.w3.org/2000/svg"><path d="M0 16c0 8.837 7.163 16 16 16s16-7.163 16-16c0-8.836-7.163-16-16-16S0 7.163 0 16zm30.032 0c0 7.72-6.312 14-14.032 14S2 23.72 2 16 8.28 2 16 2s14.032 6.28 14.032 14zM14.989 8.99v11.264l-3.617-3.617a1 1 0 0 0-1.414 1.414l6.063 5.907 6.063-5.907a.997.997 0 0 0 0-1.414 1 1 0 0 0-1.414 0l-3.68 3.68V8.991a1 1 0 1 0-2.001 0z"/></svg></span>';

        const arrowIcon = document.querySelector('.arrow-icon');
        arrowIcon.style.transform = 'translateY(-50%) rotate(0)';
        arrowIcon.classList.remove('rotated');

        const ingredient = {
            Id: parseInt(ingredientId),
            Name: name,
            Amount: parseInt(amount),
            Measurement: measurement
        };

        return ingredient;

    };

    window.deleteIngredient = async function (item) {

        const ingredient = {
            Id: parseInt(item.dataset.id),
            Name: item.dataset.name,
            Amount: parseInt(item.dataset.amount),
            Measurement: item.dataset.measurement
        };

        item.remove();

        await dotnetHelper.invokeMethodAsync('DeleteIngredient', ingredient);
        
    };
    window.editIngredient = async function (item) {
        
        document.getElementById('autoComplete').value = item.dataset.name;
        document.getElementById('autoComplete').dataset.id = item.dataset.id;
        document.getElementById('ingredient-amount').value = item.dataset.amount;

        document.getElementById('ingredient-measurement').value = item.dataset.measurement;
        let dropdown = document.querySelector('#measurement-dropdown');

        const ingredient = {
            Id: parseInt(item.dataset.id),
            Name: item.dataset.name,
            Amount: parseInt(item.dataset.amount),
            Measurement: item.dataset.measurement
        };

        const itemMeasurement = item.dataset.measurement;
        const dropdownItem = dropdown.querySelector(`.dropdown-item[data-value="${itemMeasurement}"]`);
        const dropdownText = dropdownItem ? dropdownItem.textContent : '';

        const dropdownButton = dropdown.querySelector('.dropdown-button');
        dropdownButton.innerHTML = `<div class="label">Measurement</div>${dropdownText}<span class="arrow-icon"><svg viewBox="0 0 32 32" xmlns="http://www.w3.org/2000/svg"><path d="M0 16c0 8.837 7.163 16 16 16s16-7.163 16-16c0-8.836-7.163-16-16-16S0 7.163 0 16zm30.032 0c0 7.72-6.312 14-14.032 14S2 23.72 2 16 8.28 2 16 2s14.032 6.28 14.032 14zM14.989 8.99v11.264l-3.617-3.617a1 1 0 0 0-1.414 1.414l6.063 5.907 6.063-5.907a.997.997 0 0 0 0-1.414 1 1 0 0 0-1.414 0l-3.68 3.68V8.991a1 1 0 1 0-2.001 0z"/></svg></span>`;

        let arrowIcon = document.querySelector('.arrow-icon');
        arrowIcon.style.transform = 'translateY(-50%) rotate(0)';
        arrowIcon.classList.remove('rotated');
        
        await dotnetHelper.invokeMethodAsync('EditIngredient', ingredient);
        item.remove();
        
    };
    
}

window.correctOrder = function () {
    const items = document.querySelectorAll('.ingredient-item');
    const json = Array.from(items).map(item => ({
            id: parseInt(item.dataset.id),
            name: item.dataset.name,
            amount: parseInt(item.dataset.amount),
            measurement: item.dataset.measurement
    }));
    return json;
}


window.provideIngredientsListeners = function() {
    
    document.getElementById('ingredients-list').addEventListener('click', function (e) {
        const target = e.target;
        const item = target.closest('.ingredient-item');
        if (target.classList.contains('edit-button')) {
            window.editIngredient(item);
        } else if (target.classList.contains('delete-button')) {
            item.remove();
        }
    });

    dragula([document.getElementById('ingredients-list')]);


    let dropdown = document.querySelector("#measurement-dropdown");

    dropdown.querySelector(".dropdown-button").addEventListener('click', function () {
        let dropdownMenu = this.nextElementSibling;
        if (dropdownMenu.style.display === 'block' || dropdownMenu.style.display === '') {
            dropdownMenu.style.display = 'none';
        } else {
            dropdownMenu.style.display = 'block';
        }

        let arrow = dropdown.querySelector(".arrow-icon");
        if (arrow.classList.contains("rotated")) {
            arrow.style.transform = "translateY(-50%) rotate(0)";
            arrow.classList.remove("rotated");
        } else {
            arrow.style.transform = "translateY(-50%) rotate(180deg)";
            arrow.classList.add("rotated");
        }
    });

    dropdown.querySelectorAll(".dropdown-item").forEach(item => {
        item.addEventListener('click', function () {
            let selected = this.textContent;
            let value = this.getAttribute("data-value");
            document.querySelector("#ingredient-measurement").value = value;

            let dropdownButton = dropdown.querySelector(".dropdown-button");
            dropdownButton.innerHTML = `<div class="label">Measurement</div>${selected}<span class="arrow-icon"><svg viewBox="0 0 32 32" xmlns="http://www.w3.org/2000/svg"><path d="M0 16c0 8.837 7.163 16 16 16s16-7.163 16-16c0-8.836-7.163-16-16-16S0 7.163 0 16zm30.032 0c0 7.72-6.312 14-14.032 14S2 23.72 2 16 8.28 2 16 2s14.032 6.28 14.032 14zM14.989 8.99v11.264l-3.617-3.617a1 1 0 0 0-1.414 1.414l6.063 5.907 6.063-5.907a.997.997 0 0 0 0-1.414 1 1 0 0 0-1.414 0l-3.68 3.68V8.991a1 1 0 1 0-2.001 0z"/></svg></span>`;
            document.querySelectorAll(".dropdown-menu").forEach(menu => {
                menu.style.display = 'none';
            });
            dropdown.querySelector(".arrow-icon").style.transform = "translateY(-50%) rotate(0)";
            dropdown.querySelector(".arrow-icon").classList.remove("rotated");
        });
    });

    document.addEventListener("click", function (event) {
        if (!event.target.closest("#measurement-dropdown") && !event.target.closest(".dropdown-container")) {
            document.querySelectorAll(".dropdown-menu").forEach(menu => {
                menu.style.display = 'none';
            });
            document.querySelectorAll(".arrow-icon").forEach(arrow => {
                arrow.style.transform = "translateY(-50%) rotate(0)";
                arrow.classList.remove("rotated");
            });
        }
    });
    
};



// Steps
window.provideStepsListeners = function() {

    document.getElementById("prep-steps").addEventListener("click", function (e) {
        const target = e.target;
        const item = target.closest(".step-item");
        if (target.classList.contains("edit-button")) {
            window.stepsManagement.editStep(item);
        } else if (target.classList.contains("delete-button")) {
            item.remove();
        }
    });


    document
        .getElementById("cooking-steps")
        .addEventListener("click", function (e) {
            const target = e.target;
            const item = target.closest(".step-item");
            if (target.classList.contains("edit-button")) {
                window.stepsManagement.editStep(item);
            } else if (target.classList.contains("delete-button")) {
                item.remove();
            }
        });


    dragula([
        document.getElementById("prep-steps"),
        document.getElementById("cooking-steps")
    ]);


    var stepDescription = document.getElementById("stepDescription");
    stepDescription.addEventListener("input", function () {
        this.style.height = "auto";
        this.style.height = this.scrollHeight + "px";
    });

    var dropdown = document.getElementById("phase-dropdown");
    var dropdownButton = dropdown.querySelector(".dropdown-button");
    let dropdownMenu = dropdown.querySelector(".dropdown-container .dropdown-menu");
    var arrow = dropdownButton.querySelector(".arrow-icon");

    dropdownButton.addEventListener("click", function () {
        if (dropdownMenu.style.display === 'block' || dropdownMenu.style.display === '') {
            dropdownMenu.style.display = 'none';
        } else {
            dropdownMenu.style.display = 'block';
        }

        if (arrow.classList.contains("rotated")) {
            arrow.style.transform = "translateY(-50%) rotate(0)";
            arrow.classList.remove("rotated");
        } else {
            arrow.style.transform = "translateY(-50%) rotate(180deg)";
            arrow.classList.add("rotated");
        }
    });

    var dropdownItems = dropdown.querySelectorAll(".dropdown-item");
    dropdownItems.forEach(function (item) {
        item.addEventListener("click", function () {
            var selected = this.textContent;
            var value = this.getAttribute("data-value");
            document.getElementById("step-phase").value = value;
            dropdownButton.innerHTML = '<div class="label">Phase</div>' + selected + '<span class="arrow-icon">' + arrow.innerHTML + '</span>';
            dropdownMenu.classList.remove("show");
            arrow.style.transform = "translateY(-50%) rotate(0)";
            arrow.classList.remove("rotated");
        });
    });

    document.addEventListener("click", function (event) {
        if (!event.target.closest(".dropdown-container")) {
            dropdownMenu.classList.remove("show");
            arrow.style.transform = "translateY(-50%) rotate(0)";
            arrow.classList.remove("rotated");
        }
    });

};

    window.stepsManagement = {

        addStep: function () {
            const description = document.getElementById("stepDescription").value.trim();
            const phase = document.getElementById("step-phase").value;

            if (!description || !phase) {
                alert("All fields must be filled!");
                return;
            }

            const list = (phase === "Prep") ? document.getElementById("prep-steps") : document.getElementById("cooking-steps");
            const item = document.createElement("div");
            item.className = "step-item";
            item.dataset.description = description;
            item.innerHTML = `<div class="wrapper" style="width:100%;"><div class="left-wrapper" style="display: flex; align-items: center;"><svg style="min-width: 40px;  margin-left: 0;" width="40" height="40" viewBox="0 0 48 48" fill="none" xmlns="http://www.w3.org/2000/svg"><path fill-rule="evenodd" clip-rule="evenodd" d="M17.88 15.76C18.6438 15.76 19.3764 15.4566 19.9165 14.9165C20.4566 14.3764 20.76 13.6438 20.76 12.88C20.76 12.1162 20.4566 11.3836 19.9165 10.8435C19.3764 10.3034 18.6438 10 17.88 10C17.1162 10 16.3836 10.3034 15.8435 10.8435C15.3034 11.3836 15 12.1162 15 12.88C15 13.6438 15.3034 14.3764 15.8435 14.9165C16.3836 15.4566 17.1162 15.76 17.88 15.76ZM17.88 27.28C18.2582 27.28 18.6327 27.2055 18.9821 27.0608C19.3315 26.916 19.649 26.7039 19.9165 26.4365C20.1839 26.169 20.396 25.8515 20.5408 25.5021C20.6855 25.1527 20.76 24.7782 20.76 24.4C20.76 24.0218 20.6855 23.6473 20.5408 23.2979C20.396 22.9485 20.1839 22.631 19.9165 22.3635C19.649 22.0961 19.3315 21.884 18.9821 21.7392C18.6327 21.5945 18.2582 21.52 17.88 21.52C17.1162 21.52 16.3836 21.8234 15.8435 22.3635C15.3034 22.9036 15 23.6362 15 24.4C15 25.1638 15.3034 25.8964 15.8435 26.4365C16.3836 26.9766 17.1162 27.28 17.88 27.28ZM20.76 35.92C20.76 36.6838 20.4566 37.4164 19.9165 37.9565C19.3764 38.4966 18.6438 38.8 17.88 38.8C17.1162 38.8 16.3836 38.4966 15.8435 37.9565C15.3034 37.4164 15 36.6838 15 35.92C15 35.1562 15.3034 34.4236 15.8435 33.8835C16.3836 33.3434 17.1162 33.04 17.88 33.04C18.6438 33.04 19.3764 33.3434 19.9165 33.8835C20.4566 34.4236 20.76 35.1562 20.76 35.92ZM29.4 15.76C30.1638 15.76 30.8964 15.4566 31.4365 14.9165C31.9766 14.3764 32.28 13.6438 32.28 12.88C32.28 12.1162 31.9766 11.3836 31.4365 10.8435C30.8964 10.3034 30.1638 10 29.4 10C28.6362 10 27.9036 10.3034 27.3635 10.8435C26.8234 11.3836 26.52 12.1162 26.52 12.88C26.52 13.6438 26.8234 14.3764 27.3635 14.9165C27.9036 15.4566 28.6362 15.76 29.4 15.76ZM32.28 24.4C32.28 25.1638 31.9766 25.8964 31.4365 26.4365C30.8964 26.9766 30.1638 27.28 29.4 27.28C28.6362 27.28 27.9036 26.9766 27.3635 26.4365C26.8234 25.8964 26.52 25.1638 26.52 24.4C26.52 23.6362 26.8234 22.9036 27.3635 22.3635C27.9036 21.8234 28.6362 21.52 29.4 21.52C30.1638 21.52 30.8964 21.8234 31.4365 22.3635C31.9766 22.9036 32.28 23.6362 32.28 24.4ZM29.4 38.8C30.1638 38.8 30.8964 38.4966 31.4365 37.9565C31.9766 37.4164 32.28 36.6838 32.28 35.92C32.28 35.1562 31.9766 34.4236 31.4365 33.8835C30.8964 33.3434 30.1638 33.04 29.4 33.04C28.6362 33.04 27.9036 33.3434 27.3635 33.8835C26.8234 34.4236 26.52 35.1562 26.52 35.92C26.52 36.6838 26.8234 37.4164 27.3635 37.9565C27.9036 38.4966 28.6362 38.8 29.4 38.8Z" fill="#AAAAAA" /></svg><div class="name" style="width: 90%">${description}</div></div>
          <div class="step-buttons" style="display: flex; justify-content: right; width: 100%;">
            <button class="edit-button" style="cursor: pointer; display: flex; border: 0.1px solid #eee;  box-shadow: rgba(0, 0, 0, 0.1) 0 2px 5px; padding: 15px; border-radius: 15px; background-color: #fff; margin-right: 12px;"><svg width="23" height="20" viewBox="0 0 26 23" fill="none" xmlns="http://www.w3.org/2000/svg">
                <path d="M24.52 21.9999H1" stroke="#333" stroke-width="1.5" stroke-linecap="round" />
                <path d="M6.84645 13.7344L6.04004 16.96L9.26568 16.1536C9.3536 16.1316 9.43389 16.0862 9.49797 16.0221L21.6465 3.87355C21.8417 3.67829 21.8417 3.36171 21.6465 3.16645L19.8336 1.35355C19.6383 1.15829 19.3217 1.15829 19.1265 1.35355L6.97797 13.5021C6.91389 13.5662 6.86843 13.6464 6.84645 13.7344Z" stroke="#333" stroke-width="1.5" stroke-linejoin="round" />
              </svg></button>
            <button class="delete-button" style="cursor: pointer; display: flex; border: 0.1px solid #eee; box-shadow: rgba(0, 0, 0, 0.1) 0 2px 5px; padding: 15px; border-radius: 15px; background-color: #fff; margin-right: 0!important;"><svg width="20" height="20" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                <path d="M6.09375 16.75L4.875 7L4.5 4H19.5L19 7L17.375 16.75M6.09375 16.75L6.69525 21.562C6.72653 21.8122 6.93923 22 7.19139 22H16.0764C16.3209 22 16.5295 21.8233 16.5696 21.5822L17.375 16.75M6.09375 16.75H17.375" stroke="#333" stroke-width="1.5" />
                <rect x="3" y="1" width="18" height="3" rx="1.5" stroke="#333" stroke-width="1.5" />
              </svg></button>
          </div>
        </div>`;
            
            item.querySelector('.edit-button').addEventListener('click', () => this.editStep(item));
            item.querySelector('.delete-button').addEventListener('click', () => this.deleteStep(item));

            list.appendChild(item);

            document.getElementById("stepDescription").value = "";
            document.getElementById("step-phase").value = "";
            
            let dropdown = document.getElementById("phase-dropdown");
                dropdown.querySelector(".dropdown-button").innerHTML = '<div class="label">Phase</div>' +
                    '<span class="arrow-icon">' +
                    '<svg viewBox="0 0 32 32" xmlns="http://www.w3.org/2000/svg"><path d="M0 16c0 8.837 7.163 16 16 16s16-7.163 16-16c0-8.836-7.163-16-16-16S0 7.163 0 16zm30.032 0c0 7.72-6.312 14-14.032 14S2 23.72 2 16 8.28 2 16 2s14.032 6.28 14.032 14zM14.989 8.99v11.264l-3.617-3.617a1 1 0 0 0-1.414 1.414l6.063 5.907 6.063-5.907a.997.997 0 0 0 0-1.414 1 1 0 0 0-1.414 0l-3.68 3.68V8.991a1 1 0 1 0-2.001 0z"/></svg>' +
                    '</span>';
        },

        editStep: function (item) {
            const descriptionInput = document.getElementById("stepDescription");
            descriptionInput.value = item.dataset.description;
            var parentId = item.parentNode.id;

            var phase = parentId === "prep-steps" ? "Prep" : "Cooking";
            document.getElementById("step-phase").value = phase;
            let dropdown = document.querySelector('#phase-dropdown');

            const dropdownItem = dropdown.querySelector(`.dropdown-item[data-value="${phase}"]`);
            const dropdownText = dropdownItem ? dropdownItem.textContent : '';

            const dropdownButton = dropdown.querySelector('.dropdown-button');
            dropdownButton.innerHTML =  '<div class="label">Phase</div>' +
                dropdownText +
                '<span class="arrow-icon"><svg viewBox="0 0 32 32" xmlns="http://www.w3.org/2000/svg"><path d="M0 16c0 8.837 7.163 16 16 16s16-7.163 16-16c0-8.836-7.163-16-16-16S0 7.163 0 16zm30.032 0c0 7.72-6.312 14-14.032 14S2 23.72 2 16 8.28 2 16 2s14.032 6.28 14.032 14zM14.989 8.99v11.264l-3.617-3.617a1 1 0 0 0-1.414 1.414l6.063 5.907 6.063-5.907a.997.997 0 0 0 0-1.414 1 1 0 0 0-1.414 0l-3.68 3.68V8.991a1 1 0 1 0-2.001 0z"/></svg></span>';
            let arrowIcon = document.querySelector('.arrow-icon');
            arrowIcon.style.transform = 'translateY(-50%) rotate(0)';
            arrowIcon.classList.remove('rotated');
            
            if (item instanceof HTMLElement) {
                item.remove();
            }
            
        },
        
        deleteStep: function (item) {
            item.remove();
        }
        
        
    };
    
    window.getStepsJson = function () {
        const prepItems = document.querySelectorAll("#prep-steps .step-item");
        const cookingItems = document.querySelectorAll("#cooking-steps .step-item");

        const prepSteps = Array.from(prepItems).map((item, index) => ({
            description: item.dataset.description,
            phase: "Prep",
            stepNumber: index + 1
        }));

        const cookingSteps = Array.from(cookingItems).map((item, index) => ({
            description: item.dataset.description,
            phase: "Cooking",
            stepNumber: prepSteps.length + index + 1
        }));
        
        return [...prepSteps, ...cookingSteps];

    };

    
    function loadSteps(steps) {
        
        const prepSteps = steps.filter(step => step.phase === "Prep");
        const cookingSteps = steps.filter(step => step.phase === "Cooking");
        const prepList = document.getElementById("prep-steps");
        const cookingList = document.getElementById("cooking-steps");

        prepSteps.forEach(step => {
            let description = step.description.replace(/\n/g, ' ');
            if (!document.querySelector(`.step-item[data-description="${description}"]`)) {

                const item = document.createElement("div");
                item.className = "step-item";
                item.dataset.description = step.description;
                item.innerHTML = `<div class="wrapper" style="width:100%;"><div class="left-wrapper" style="display: flex; align-items: center;">
                <svg style="min-width: 40px;  margin-left: 0;" width="40" height="40" viewBox="0 0 48 48" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path fill-rule="evenodd" clip-rule="evenodd" d="M17.88 15.76C18.6438 15.76 19.3764 15.4566 19.9165 14.9165C20.4566 14.3764 20.76 13.6438 20.76 12.88C20.76 12.1162 20.4566 11.3836 19.9165 10.8435C19.3764 10.3034 18.6438 10 17.88 10C17.1162 10 16.3836 10.3034 15.8435 10.8435C15.3034 11.3836 15 12.1162 15 12.88C15 13.6438 15.3034 14.3764 15.8435 14.9165C16.3836 15.4566 17.1162 15.76 17.88 15.76ZM17.88 27.28C18.2582 27.28 18.6327 27.2055 18.9821 27.0608C19.3315 26.916 19.649 26.7039 19.9165 26.4365C20.1839 26.169 20.396 25.8515 20.5408 25.5021C20.6855 25.1527 20.76 24.7782 20.76 24.4C20.76 24.0218 20.6855 23.6473 20.5408 23.2979C20.396 22.9485 20.1839 22.631 19.9165 22.3635C19.649 22.0961 19.3315 21.884 18.9821 21.7392C18.6327 21.5945 18.2582 21.52 17.88 21.52C17.1162 21.52 16.3836 21.8234 15.8435 22.3635C15.3034 22.9036 15 23.6362 15 24.4C15 25.1638 15.3034 25.8964 15.8435 26.4365C16.3836 26.9766 17.1162 27.28 17.88 27.28ZM20.76 35.92C20.76 36.6838 20.4566 37.4164 19.9165 37.9565C19.3764 38.4966 18.6438 38.8 17.88 38.8C17.1162 38.8 16.3836 38.4966 15.8435 37.9565C15.3034 37.4164 15 36.6838 15 35.92C15 35.1562 15.3034 34.4236 15.8435 33.8835C16.3836 33.3434 17.1162 33.04 17.88 33.04C18.6438 33.04 19.3764 33.3434 19.9165 33.8835C20.4566 34.4236 20.76 35.1562 20.76 35.92ZM29.4 15.76C30.1638 15.76 30.8964 15.4566 31.4365 14.9165C31.9766 14.3764 32.28 13.6438 32.28 12.88C32.28 12.1162 31.9766 11.3836 31.4365 10.8435C30.8964 10.3034 30.1638 10 29.4 10C28.6362 10 27.9036 10.3034 27.3635 10.8435C26.8234 11.3836 26.52 12.1162 26.52 12.88C26.52 13.6438 26.8234 14.3764 27.3635 14.9165C27.9036 15.4566 28.6362 15.76 29.4 15.76ZM32.28 24.4C32.28 25.1638 31.9766 25.8964 31.4365 26.4365C30.8964 26.9766 30.1638 27.28 29.4 27.28C28.6362 27.28 27.9036 26.9766 27.3635 26.4365C26.8234 25.8964 26.52 25.1638 26.52 24.4C26.52 23.6362 26.8234 22.9036 27.3635 22.3635C27.9036 21.8234 28.6362 21.52 29.4 21.52C30.1638 21.52 30.8964 21.8234 31.4365 22.3635C31.9766 22.9036 32.28 23.6362 32.28 24.4ZM29.4 38.8C30.1638 38.8 30.8964 38.4966 31.4365 37.9565C31.9766 37.4164 32.28 36.6838 32.28 35.92C32.28 35.1562 31.9766 34.4236 31.4365 33.8835C30.8964 33.3434 30.1638 33.04 29.4 33.04C28.6362 33.04 27.9036 33.3434 27.3635 33.8835C26.8234 34.4236 26.52 35.1562 26.52 35.92C26.52 36.6838 26.8234 37.4164 27.3635 37.9565C27.9036 38.4966 28.6362 38.8 29.4 38.8Z" fill="#AAAAAA" />
                </svg>
                <div class="name" style="width: 90%">${step.description}</div>
            </div>
                <div class="step-buttons" style="display: flex; justify-content: right; width: 100%;">
                    <button class="edit-button" style="cursor: pointer; display: flex; border: 0.1px solid #eee;  box-shadow: rgba(0, 0, 0, 0.1) 0 2px 5px; padding: 15px; border-radius: 15px; background-color: #fff; margin-right: 12px;"><svg width="23" height="20" viewBox="0 0 26 23" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <path d="M24.52 21.9999H1" stroke="#333" stroke-width="1.5" stroke-linecap="round" />
                        <path d="M6.84645 13.7344L6.04004 16.96L9.26568 16.1536C9.3536 16.1316 9.43389 16.0862 9.49797 16.0221L21.6465 3.87355C21.8417 3.67829 21.8417 3.36171 21.6465 3.16645L19.8336 1.35355C19.6383 1.15829 19.3217 1.15829 19.1265 1.35355L6.97797 13.5021C6.91389 13.5662 6.86843 13.6464 6.84645 13.7344Z" stroke="#333" stroke-width="1.5" stroke-linejoin="round" />
                    </svg></button>
                    <button class="delete-button" style="cursor: pointer; display: flex; border: 0.1px solid #eee; box-shadow: rgba(0, 0, 0, 0.1) 0 2px 5px; padding: 15px; border-radius: 15px; background-color: #fff; margin-right: 0!important;"><svg width="20" height="20" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <path d="M6.09375 16.75L4.875 7L4.5 4H19.5L19 7L17.375 16.75M6.09375 16.75L6.69525 21.562C6.72653 21.8122 6.93923 22 7.19139 22H16.0764C16.3209 22 16.5295 21.8233 16.5696 21.5822L17.375 16.75M6.09375 16.75H17.375" stroke="#333" stroke-width="1.5" />
                        <rect x="3" y="1" width="18" height="3" rx="1.5" stroke="#333" stroke-width="1.5" />
                    </svg></button>
                </div>
            </div>`;

                item.querySelector('.edit-button').addEventListener('click', () => window.stepsManagement.editStep(item));
                item.querySelector('.delete-button').addEventListener('click', () => window.stepsManagement.deleteStep(item));
                prepList.appendChild(item);
            }

        });
        
        cookingSteps.forEach(step => {
            let description = step.description.replace(/\n/g, ' ');
            if (!document.querySelector(`.step-item[data-description="${description}"]`)) {
    
                const item = document.createElement("div");
                item.className = "step-item";
                item.dataset.description = step.description;
                item.innerHTML = `<div class="wrapper" style="width:100%;"><div class="left-wrapper" style="display: flex; align-items: center;">
                    <svg style="min-width: 40px;  margin-left: 0;" width="40" height="40" viewBox="0 0 48 48" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <path fill-rule="evenodd" clip-rule="evenodd" d="M17.88 15.76C18.6438 15.76 19.3764 15.4566 19.9165 14.9165C20.4566 14.3764 20.76 13.6438 20.76 12.88C20.76 12.1162 20.4566 11.3836 19.9165 10.8435C19.3764 10.3034 18.6438 10 17.88 10C17.1162 10 16.3836 10.3034 15.8435 10.8435C15.3034 11.3836 15 12.1162 15 12.88C15 13.6438 15.3034 14.3764 15.8435 14.9165C16.3836 15.4566 17.1162 15.76 17.88 15.76ZM17.88 27.28C18.2582 27.28 18.6327 27.2055 18.9821 27.0608C19.3315 26.916 19.649 26.7039 19.9165 26.4365C20.1839 26.169 20.396 25.8515 20.5408 25.5021C20.6855 25.1527 20.76 24.7782 20.76 24.4C20.76 24.0218 20.6855 23.6473 20.5408 23.2979C20.396 22.9485 20.1839 22.631 19.9165 22.3635C19.649 22.0961 19.3315 21.884 18.9821 21.7392C18.6327 21.5945 18.2582 21.52 17.88 21.52C17.1162 21.52 16.3836 21.8234 15.8435 22.3635C15.3034 22.9036 15 23.6362 15 24.4C15 25.1638 15.3034 25.8964 15.8435 26.4365C16.3836 26.9766 17.1162 27.28 17.88 27.28ZM20.76 35.92C20.76 36.6838 20.4566 37.4164 19.9165 37.9565C19.3764 38.4966 18.6438 38.8 17.88 38.8C17.1162 38.8 16.3836 38.4966 15.8435 37.9565C15.3034 37.4164 15 36.6838 15 35.92C15 35.1562 15.3034 34.4236 15.8435 33.8835C16.3836 33.3434 17.1162 33.04 17.88 33.04C18.6438 33.04 19.3764 33.3434 19.9165 33.8835C20.4566 34.4236 20.76 35.1562 20.76 35.92ZM29.4 15.76C30.1638 15.76 30.8964 15.4566 31.4365 14.9165C31.9766 14.3764 32.28 13.6438 32.28 12.88C32.28 12.1162 31.9766 11.3836 31.4365 10.8435C30.8964 10.3034 30.1638 10 29.4 10C28.6362 10 27.9036 10.3034 27.3635 10.8435C26.8234 11.3836 26.52 12.1162 26.52 12.88C26.52 13.6438 26.8234 14.3764 27.3635 14.9165C27.9036 15.4566 28.6362 15.76 29.4 15.76ZM32.28 24.4C32.28 25.1638 31.9766 25.8964 31.4365 26.4365C30.8964 26.9766 30.1638 27.28 29.4 27.28C28.6362 27.28 27.9036 26.9766 27.3635 26.4365C26.8234 25.8964 26.52 25.1638 26.52 24.4C26.52 23.6362 26.8234 22.9036 27.3635 22.3635C27.9036 21.8234 28.6362 21.52 29.4 21.52C30.1638 21.52 30.8964 21.8234 31.4365 22.3635C31.9766 22.9036 32.28 23.6362 32.28 24.4ZM29.4 38.8C30.1638 38.8 30.8964 38.4966 31.4365 37.9565C31.9766 37.4164 32.28 36.6838 32.28 35.92C32.28 35.1562 31.9766 34.4236 31.4365 33.8835C30.8964 33.3434 30.1638 33.04 29.4 33.04C28.6362 33.04 27.9036 33.3434 27.3635 33.8835C26.8234 34.4236 26.52 35.1562 26.52 35.92C26.52 36.6838 26.8234 37.4164 27.3635 37.9565C27.9036 38.4966 28.6362 38.8 29.4 38.8Z" fill="#AAAAAA" />
                    </svg>
                    <div class="name" style="width: 90%">${step.description}</div>
                </div>
                    <div class="step-buttons" style="display: flex; justify-content: right; width: 100%;">
                        <button class="edit-button" style="cursor: pointer; display: flex; border: 0.1px solid #eee;  box-shadow: rgba(0, 0, 0, 0.1) 0 2px 5px; padding: 15px; border-radius: 15px; background-color: #fff; margin-right: 12px;"><svg width="23" height="20" viewBox="0 0 26 23" fill="none" xmlns="http://www.w3.org/2000/svg">
                            <path d="M24.52 21.9999H1" stroke="#333" stroke-width="1.5" stroke-linecap="round" />
                            <path d="M6.84645 13.7344L6.04004 16.96L9.26568 16.1536C9.3536 16.1316 9.43389 16.0862 9.49797 16.0221L21.6465 3.87355C21.8417 3.67829 21.8417 3.36171 21.6465 3.16645L19.8336 1.35355C19.6383 1.15829 19.3217 1.15829 19.1265 1.35355L6.97797 13.5021C6.91389 13.5662 6.86843 13.6464 6.84645 13.7344Z" stroke="#333" stroke-width="1.5" stroke-linejoin="round" />
                        </svg></button>
                        <button class="delete-button" style="cursor: pointer; display: flex; border: 0.1px solid #eee; box-shadow: rgba(0, 0, 0, 0.1) 0 2px 5px; padding: 15px; border-radius: 15px; background-color: #fff; margin-right: 0!important;"><svg width="20" height="20" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                            <path d="M6.09375 16.75L4.875 7L4.5 4H19.5L19 7L17.375 16.75M6.09375 16.75L6.69525 21.562C6.72653 21.8122 6.93923 22 7.19139 22H16.0764C16.3209 22 16.5295 21.8233 16.5696 21.5822L17.375 16.75M6.09375 16.75H17.375" stroke="#333" stroke-width="1.5" />
                            <rect x="3" y="1" width="18" height="3" rx="1.5" stroke="#333" stroke-width="1.5" />
                        </svg></button>
                    </div>
                </div>`;
    
                item.querySelector('.edit-button').addEventListener('click', () => window.stepsManagement.editStep(item));
                item.querySelector('.delete-button').addEventListener('click', () => window.stepsManagement.deleteStep(item));
                cookingList.appendChild(item);
            }

        });
        
            
    }
    
  





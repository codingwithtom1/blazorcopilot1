
function UpdateScroll(elementid) {
    var element = document.getElementById(elementid);
    setTimeout(() => {
        element.scrollTop = element.scrollHeight;
    }, 100);
}


function submitOnEnter(event) {
    if (event.which === 13) {
        if (!event.repeat) {
            const newEvent = new Event("submit", {cancelable: true});
            event.target.form.dispatchEvent(newEvent);
        }
        event.preventDefault(); // Prevents the addition of a new line in the text field
    }
}

function addSubmitOnEnter(element) {
    document.getElementById(element).addEventListener("keydown", submitOnEnter);
}


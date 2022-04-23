function SelectMotherboards(allProcessors, allMotherboards) {
    let processorField = document.getElementById('ProcessorId')
    let motherboardField = document.getElementById('MotherboardId')

    let processorFieldValue = processorField.options[processorField.selectedIndex].value
    let currentProcessor = null

    //remove old motherboard
    motherboardField.length = 0

    //search processord in collection
    for (var i = 0; i < allProcessors.length; i++) {
        if (processorFieldValue == allProcessors[i].id) {
            currentProcessor = allProcessors[i]
            break
        }
    }

    //search for compatible motherboards
    for (var i = 0; i < allMotherboards.length; i++) {
        if (allMotherboards[i].socket == currentProcessor.socket) {
            let newOption = new Option(allMotherboards[i].model, allMotherboards[i].id)
            motherboardField.add(newOption, undefined);
        }
    }
}

function SelectRams(allMotherboards, allRams) {
    let ramField = document.getElementById('RamId')
    let motherboardField = document.getElementById('MotherboardId')

    let motherboardFieldValue = motherboardField.options[motherboardField.selectedIndex].value
    let currentMotherboard = null
    ramField.length = 0

    //search motherboard in collection
    for (var i = 0; i < allMotherboards.length; i++) {
        if (motherboardFieldValue == allMotherboards[i].id) {
            currentMotherboard = allMotherboards[i]
            break
        }
    }

    //search for compatible rams
    for (var i = 0; i < allRams.length; i++) {
        if (allRams[i].typeRam == currentMotherboard.typeRam) {
            let newOption1 = new Option(allRams[i].model, allRams[i].id)
            ramField.add(newOption1, undefined);
        }
    }
}

function isUsernameFree(usernames) {
    let currentUsername = document.getElementById('username').value

    for (var i = 0; i < usernames.length; i++) {
        if (usernames[i] == currentUsername) {
            let span = document.getElementById('usernameEror')
            span.className = 'text-danger'
            span.textContent = "User exist!"
            return false
        }
    }
    document.getElementById('usernameEror').className += ' d-none'
    return true
}
function visitShop(shop) {
    if (shop.id == "shop1") {
        var target = document.getElementById("map");
        target.innerHTML = '<iframe class="col-12 rounded-2"src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2936.550446046061!2d23.030714314841394!3d42.6072807274467!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x14aacac3bbc30045%3A0x1282864432d70ccb!2z0J_QnNCTICLQpdGA0LjRgdGC0L4g0KHQvNC40YDQvdC10L3RgdC60Lgi!5e0!3m2!1sen!2sbg!4v1650729965667!5m2!1sen!2sbg" width="600" height="450" style="border:0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>';
    }
    else if (shop.id == "shop2") {
        var target = document.getElementById("map");
        target.innerHTML = '<iframe class="col-12 rounded-2" src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2936.550446046061!2d23.030714314841394!3d42.6072807274467!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x14aacac3bbc30045%3A0x1282864432d70ccb!2z0J_QnNCTICLQpdGA0LjRgdGC0L4g0KHQvNC40YDQvdC10L3RgdC60Lgi!5e0!3m2!1sen!2sbg!4v1650729965667!5m2!1sen!2sbg" width="600" height="450" style="border:0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>';

    }
    else if (shop.id == "shop3") {
        var target = document.getElementById("map");
        target.innerHTML = '<iframe class="col-12 rounded-2" src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2936.550446046061!2d23.030714314841394!3d42.6072807274467!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x14aacac3bbc30045%3A0x1282864432d70ccb!2z0J_QnNCTICLQpdGA0LjRgdGC0L4g0KHQvNC40YDQvdC10L3RgdC60Lgi!5e0!3m2!1sen!2sbg!4v1650729965667!5m2!1sen!2sbg" width="600" height="450" style="border:0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>';
    }
}
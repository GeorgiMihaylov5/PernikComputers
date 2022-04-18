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
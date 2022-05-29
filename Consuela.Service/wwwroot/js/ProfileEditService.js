var _global = {
    Ignore: {
        Files: 0,
        Directories: 0
    }
};

function btnAddFileOnClick() {
    var txt = document.getElementById("txtAddFile");
    var tbody = document.getElementById("tbodyIgnoreFiles");

    var rIndex = tbody.rows.length;

    //New rows are different from existing, denoted by the _N versus _E
    var rowId = "trIgnoreFiles_N" + _global.Ignore.Files;

    _global.Ignore.Files++;

    // Create an empty <tr> element and add it to the end of the table
    var row = tbody.insertRow(rIndex);
    row.id = rowId;

    // Create new cells to fill the row
    var tdFile = row.insertCell(0);
    var tdX = row.insertCell(1);

    var btn = document.createElement("button");
    btn.textContent = "X";
    btn.addEventListener("click", function(){ btnRemoveFileOnClick(rowId); });

    // Add some text to the new cells:
    tdFile.innerHTML = txt.value;
    tdX.appendChild(btn);
}

function btnRemoveFileOnClick(rowId) {
    document.getElementById(rowId).remove();
}
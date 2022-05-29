var _global = {
    RowCount: 0
};

function btnAddIgnoreFileOnClick() {
    btnAddItemOnClick("txtAddIgnoreFile", "tbodyIgnoreFiles", "trIgnoreFiles");
}

function btnAddIgnoreDirectoryOnClick() {
    btnAddItemOnClick("txtAddIgnoreDirectory", "tbodyIgnoreDirectories", "trIgnoreDirectories");
}

function btnAddDeletePathsOnClick() {
    btnAddItemOnClick("txtAddDeletePath", "tbodyDeletePaths", "trDeletePaths", function (row) {
        var txt = document.getElementById("txtAddDeletePattern");

        // Pattern column
        var tdStringCol = row.insertCell(1);
        tdStringCol.innerHTML = txt.value;
    });
}

function btnAddItemOnClick(txtId, tbodyId, trPrefix, additionalColumns) {
    var txt = document.getElementById(txtId);
    var tbody = document.getElementById(tbodyId);

    var rIndex = tbody.rows.length;

    //New rows are different from existing, denoted by the _N versus _E
    var rowId = trPrefix + "_N" + _global.RowCount;

    _global.RowCount++;

    // Create an empty <tr> element and add it to the end of the table
    var row = tbody.insertRow(rIndex);
    row.id = rowId;

    // String column is at the beginning
    var tdStringCol = row.insertCell(0);
    tdStringCol.innerHTML = txt.value;

    // Optional additional columns
    if (additionalColumns) {
        additionalColumns(row);
    }

    // Delete button should always be at the end
    var btn = document.createElement("button");
    btn.textContent = "X";
    btn.addEventListener("click", function(){ btnRemoveItemOnClick(rowId); });

    var tdX = row.insertCell(-1);
    tdX.appendChild(btn);
}

function btnRemoveItemOnClick(rowId) {
    document.getElementById(rowId).remove();
}

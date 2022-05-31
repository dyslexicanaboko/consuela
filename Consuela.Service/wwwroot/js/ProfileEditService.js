var _global = {
    RowCount: 0
};

function btnAddIgnoreFileOnClick() {
    btnAddItemOnClick("txtAddIgnoreFile", "IgnoreFiles", "tbodyIgnoreFiles", "trIgnoreFiles");
}

function btnAddIgnoreDirectoryOnClick() {
    btnAddItemOnClick("txtAddIgnoreDirectory", "IgnoreDirectories", "tbodyIgnoreDirectories", "trIgnoreDirectories");
}

function btnAddDeletePathsOnClick() {
    btnAddItemOnClick("custom", "DeletePaths", "tbodyDeletePaths", "trDeletePaths", function (row, propertyName, rowIndex) {
        //Path column
        var tdPathTxt = createTextBox(propertyName, rowIndex, "txtAddDeletePath", ".Path");

        addCellWithTextBox(row, 0, tdPathTxt);

        // Pattern column
        var tdPatternTxt = createTextBox(propertyName, rowIndex, "txtAddDeletePattern", ".Pattern");

        addCellWithTextBox(row, 1, tdPatternTxt);
    });
}

function addCellWithTextBox(row, cellIndex, outputTxt) {
    var td = row.insertCell(cellIndex);
    td.appendChild(outputTxt);
}

function btnAddItemOnClick(txtId, propertyName, tbodyId, trPrefix, additionalColumns) {
    var tbody = document.getElementById(tbodyId);

    //var rIndex = tbody.rows.length;

    //New rows are different from existing, denoted by the _N versus _E
    var rowIndex = _global.RowCount;
    var rowId = trPrefix + "_N" + _global.RowCount;

    _global.RowCount++;

    // Create an empty <tr> element and add it to the end of the table
    var row = tbody.insertRow(-1);
    row.id = rowId;

    // Optional additional columns
    if (additionalColumns) {
        additionalColumns(row, propertyName, rowIndex);
    } else {
        // String column is at the beginning
        var tdStringTxt = createTextBox(propertyName, rowIndex, txtId, "");

        addCellWithTextBox(row, 0, tdStringTxt);
    }

    // Delete button should always be at the end
    var btn = document.createElement("button");
    btn.textContent = "X";
    btn.addEventListener("click", function(){ btnRemoveItemOnClick(rowId); });

    var tdX = row.insertCell(-1);
    tdX.appendChild(btn);
}

function createTextBox(propertyName, rowIndex, inputTxtId, suffix) {
    var inputTxt = document.getElementById(inputTxtId);

    var txt = document.createElement("input");
    txt.type = "text";
    txt.name = propertyName + "[" + rowIndex + "]" + suffix;
    txt.value = inputTxt.value;

    return txt;
}

function btnRemoveItemOnClick(rowId) {
    document.getElementById(rowId).remove();
}

function setFrequencyByValue() {
    var enumValue = parseInt(document.getElementById("hdnFrequency").value);

    var ddl = document.getElementById("ddlFrequency");

    ddl.value = enumValue;
}

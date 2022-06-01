var _global = {
    rowCount: 0,
    ignore: {
        files: {
            name: "IgnoreFiles",
            namePrefix: "IgnoreFiles",
            nameSuffix: "",
            txtAddId: "txtAddIgnoreFile",
            tbodyId: "tbodyIgnoreFiles",
            trPrefix: "trIgnoreFiles"
        },
        directories: {
            name: "IgnoreDirectories",
            namePrefix: "IgnoreDirectories",
            nameSuffix: "",
            txtAddId: "txtAddIgnoreDirectory",
            tbodyId: "tbodyIgnoreDirectories",
            trPrefix: "trIgnoreDirectories"
        }
    },
    delete: {
        paths: {
            path: {
                name: "DeletePaths.Path",
                namePrefix: "DeletePaths",
                nameSuffix: ".Path",
                txtAddId: "txtAddDeletePath",
                tbodyId: "tbodyDeletePaths",
                trPrefix: "trDeletePaths"
            },
            pattern: {
                name: "DeletePaths.Pattern",
                namePrefix: "DeletePaths",
                nameSuffix: ".Pattern",
                txtAddId: "txtAddDeletePattern",
                tbodyId: "tbodyDeletePaths",
                trPrefix: "trDeletePaths"
            }
        }
    }
};

function btnAddIgnoreFileOnClick() {
    btnAddItemOnClick(_global.ignore.files);
}

function btnAddIgnoreDirectoryOnClick() {
    btnAddItemOnClick(_global.ignore.directories);
}

function btnAddDeletePathsOnClick() {
    //Using the path arbitrarily because it doesn't matter in this case since
    //the function overrides the default behavior
    btnAddItemOnClick(_global.delete.paths.path, function (row) {
        //Path column
        var tdPathTxt = createTextBox(_global.delete.paths.path);

        addCellWithTextBox(row, 0, tdPathTxt);

        // Pattern column
        var tdPatternTxt = createTextBox(_global.delete.paths.pattern);

        addCellWithTextBox(row, 1, tdPatternTxt);
    });
}

function addCellWithTextBox(row, cellIndex, outputTxt) {
    var td = row.insertCell(cellIndex);
    td.appendChild(outputTxt);
}

function btnAddItemOnClick(indexedProperty, additionalColumns) {
    var tbody = document.getElementById(indexedProperty.tbodyId);

    //New rows are different from existing, denoted by the _N versus _E
    var rowId = indexedProperty.trPrefix + "_N" + _global.rowCount;

    _global.rowCount++;

    // Create an empty <tr> element and add it to the end of the table
    var row = tbody.insertRow(-1);
    row.id = rowId;

    // Optional additional columns
    if (additionalColumns) {
        additionalColumns(row);
    } else {
        // String column is at the beginning
        var tdStringTxt = createTextBox(indexedProperty);

        addCellWithTextBox(row, 0, tdStringTxt);
    }

    // Delete button should always be at the end
    var btn = document.createElement("button");
    btn.textContent = "X";
    btn.addEventListener("click", function(){ btnRemoveItemOnClick(rowId); });

    var tdX = row.insertCell(-1);
    tdX.appendChild(btn);
}

function createTextBox(indexedProperty) {
    var txtAdd = document.getElementById(indexedProperty.txtAddId);

    var txt = document.createElement("input");
    txt.type = "text";
    txt.name = indexedProperty.name;
    txt.value = txtAdd.value;

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

function setIndexedNames(indexedProperty) {
    setIndexedNamesR(indexedProperty, 0);
}

function setIndexedNamesR(indexedProperty, i) {
    var names = document.getElementsByName(indexedProperty.name);

    console.dir(names);

    //When there are no more names then stop recursing
    if (names === null || names === undefined || names.length == 0) {
        return;
    }

    //https://developer.mozilla.org/en-US/docs/Web/API/NodeList/item
    //Just skim off the top, always select the first item in the NodeList
    var prop = names.item(0);

    console.log(names.length);

    //The moment the elements's name is changed the NodeList loses the reference
    prop.name = indexedProperty.namePrefix + "[" + i + "]" + indexedProperty.nameSuffix;

    console.log(prop.name);

    i++;

    //Recurse until no more nodes are found
    setIndexedNamesR(indexedProperty, i);
}

function submitForm() {
    //var form = document.getElementById("editForm");

    //For RazorPages to serialize any of these table rows as a List<T> it's 
    //imperative that the index is sequential and non - repeating
    setIndexedNames(_global.ignore.files);
    setIndexedNames(_global.ignore.directories);
    setIndexedNames(_global.delete.paths.path);
    setIndexedNames(_global.delete.paths.pattern);

    //form.submit();
}

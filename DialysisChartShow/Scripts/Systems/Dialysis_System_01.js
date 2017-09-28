//# sourceURL=Dialysis_System_01.js

function Dialysis_System_01_Handler() {
    this.onAdd = function () {
        // ui updates
        App.btnAdd.disable();
        App.btnExportMedicine.disable();
        App.btnCANCEL.enable();
        App.btnSave.enable();
        
        // get the category combobox value
        var valCategory = App.ComboBoxGroup.getValue();

        if (valCategory == "(全部)") {
            App.Store2.insert(0, {});
        } else {
            App.Store2.insert(0, {
                drg_grp: valCategory
            });
        }


        App.GridPanel1.editingPlugin.startEditByPosition({ row: 0, column: 2 });
    };
}

var handlerDialsysiSystem01 = new Dialysis_System_01_Handler();
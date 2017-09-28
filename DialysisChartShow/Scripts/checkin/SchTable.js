//# sourceURL=SchTable.js

function SchTable_Handler() {
    this.onSave = function () {
        var store = App.Store1;
        var updates = store.getUpdatedRecords();
    };

    this.onOverride = function () {
        var gv = App.GridView1;
        var cdd = App.CellDragDrop1;
        var dropZone = cdd.dropZone;
        var parentOnNodeEnter = dropZone.onNodeEnter;

        dropZone.onNodeEnter = function (target, dd, e, dragData) {
            var destType = target.columnName ? target.record.getField(target.columnName).type.toUpperCase() : null,
            sourceType = dragData.columnName ? dragData.record.getField(dragData.columnName).type.toUpperCase() : null;
            var dragName = dragData.record.get(dragData.columnName).Name;
            if (dragName.indexOf("(s)") > -1) {
                return
            }
            delete dropZone.dropOK;
            if (!target || target.node === dragData.item.parentNode) {
                return;
            }

            // our validation codes, cannot switch betweewn diff weekday
            if (destType == null || dropZone.enforceType && destType !== sourceType || target.columnName != dragData.columnName) {
                dropZone.dropOK = false;
                if (dropZone.noDropCls) {
                    Ext.fly(target.node).addCls(dropZone.noDropCls);
                } else {
                    Ext.fly(target.node).applyStyles({
                        backgroundColor: dropZone.noDropBackgroundColor
                    });
                }
                return false;
            }
            var tarName = target.record.get(target.columnName).Name;
            if (tarName.indexOf("(s)") > -1) {
                return false;
            }
            dropZone.dropOK = true;
            if (dropZone.dropCls) {
                Ext.fly(target.node).addCls(dropZone.dropCls);
            } else {
                Ext.fly(target.node).applyStyles({
                    backgroundColor: dropZone.dropBackgroundColor
                });
            }
        };

        dropZone.onNodeDrop = function (target, dd, e, dragData) {
            if ((dragData.record.get(target.columnName).Name).indexOf("(s)") > -1) {
                this.dropOK = false;
            }
            if (this.dropOK) {
                // get and set drag source data.
                var tarValue = target.record.get(target.columnName);
                target.record.set(target.columnName, dragData.record.get(dragData.columnName));
                if (dropZone.applyEmptyText) {
                    dragData.record.set(dragData.columnName, dropZone.emptyText);
                } else {
                    dragData.record.set(dragData.columnName, tarValue);
                }
                return true;
            }
        };
    };
}

var handlerSchTable = new SchTable_Handler();
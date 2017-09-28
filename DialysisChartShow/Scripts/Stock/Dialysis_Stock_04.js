//# sourceURL=Dialysis_Stock_04.js

function Dialysis_Stock_04_Handler() {
    this.onBtnSaveReeturnBefore = function () {
        var data = App.Store2.getData();

        for (var i = 0; i < data.length; i++) {
            var curItem = data.getAt(i);
            if (curItem.data.RetAmt == null) {
                curItem.set('RetAmt', 0);
            }
        }

        //return App.Store2.isDirty();
        return true;
    };

    this.onViewReady = function () {
        /* 暫時用這個方法 global inheritance 還不知發生什麼事，無法運作 */
        App.RowEditingReturn.saveBtnText = '更新';
        App.RowEditingReturn.cancelBtnText = '取消';
        App.RowEditingReturn.errorsText = '错误';
        App.RowEditingReturn.dirtyText = '你需要先保存或者取消你的变更。';
    };

    this.onValidateEdit = function (editor, context, eOpts) {
        if (isNaN(context.newValues.RetAmt)) {
            Ext.MessageBox.alert('错误信息', '请输入数值。');
            return false;
        }

        var oldVal = parseInt(context.record.get('PreAmt'));
        var newVal = parseInt(context.newValues.RetAmt);

        if (newVal > oldVal) {
            Ext.MessageBox.alert('错误信息', '退回数量大于领用数量。');
            return false;
        }

        return true;
    };

    this.onBeforeEdit = function (editor, context, eOpts) {
        var id = context.record.get('Id');
        if (id == null || isNaN(id)) {
            // id not a number means the record is runtime generated, not saved in table
            return true;
        } else {
            Ext.MessageBox.alert('系统信息', '退料单已保存，无法修改。');
            return false;
        }
    };
}

var handlerDialysisStock04 = new Dialysis_Stock_04_Handler();
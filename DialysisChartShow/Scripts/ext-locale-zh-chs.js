/* not work, we will be load after the javascript codes of the view loaded.  */
if (Ext.grid.RowEditor) {
    Ext.apply(Ext.grid.RowEditor.prototype, {
        saveBtnText: '更新',
        cancelBtnText: '取消',
        errorsText: '错误',
        dirtyText: '你需要先保存或者取消你的变更。'
    });
}
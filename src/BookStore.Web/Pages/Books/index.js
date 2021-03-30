/*
    abp.localization.getResource 获取一个函数,该函数用于使用服务器端定义的相同JSON文件对文本进行本地化. 通过这种方式你可以与客户端共享本地化值.

    abp.libs.datatables.normalizeConfiguration是另一个辅助方法.不是必须的, 但是它通过为缺少的选项提供常规值来简化数据表配置.

    abp.libs.datatables.createAjax是帮助ABP的动态JavaScript API代理跟Datatable的格式相适应的辅助方法.

    bookStore.books.book.getList 是动态JavaScript代理函数

    luxon 库也是该解决方案中预先配置的标准库,你可以轻松地执行日期/时间操作

    https://datatables.net/manual/
 */

$(function () {
    var l = abp.localization.getResource('BookStore');

    var dataTable = $('#BooksTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(bookStore.books.book.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible: abp.auth.isGranted('BookStore.Books.Edit'), //CHECK for the PERMISSION
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted('BookStore.Books.Delete'),
                                    confirmMessage: function (data) {
                                        return l('BookDeletionConfirmationMessage', data.record.name);
                                    },
                                    action: function (data) {
                                        acme.bookStore.books.book
                                            .delete(data.record.id)
                                            .then(function () {
                                                abp.notify.info(l('SuccessfullyDeleted'));
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                },
                {
                    title: l('Name'),
                    data: "name"
                },
                {
                    title: l('Type'),
                    data: "type",
                    render: function (data) {
                        return l('Enum:BookType:' + data);
                    }
                },
                {
                    title: l('PublishDate'),
                    data: "publishDate",
                    render: function (data) {
                        return luxon
                            .DateTime
                            .fromISO(data, {
                                locale: abp.localization.currentCulture.name
                            }).toLocaleString();
                    }
                },
                {
                    title: l('Price'),
                    data: "price"
                },
                {
                    title: l('CreationTime'), data: "creationTime",
                    render: function (data) {
                        return luxon
                            .DateTime
                            .fromISO(data, {
                                locale: abp.localization.currentCulture.name
                            }).toLocaleString(luxon.DateTime.DATETIME_SHORT);
                    }
                }
            ]
        })
    );


    /*
        abp.ModalManager 是一个在客户端打开和管理modal的辅助类.它基于Twitter Bootstrap的标准modal组件通过简化的API抽象隐藏了许多细节.
        createModal.onResult(...) 用于在创建书籍后刷新数据表格.
        createModal.open(); 用于打开模态创建新书籍.

    */
    var createModal = new abp.ModalManager(abp.appPath + 'Books/CreateModal');

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });


    var editModal = new abp.ModalManager(abp.appPath + 'Books/EditModal');

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewBookButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
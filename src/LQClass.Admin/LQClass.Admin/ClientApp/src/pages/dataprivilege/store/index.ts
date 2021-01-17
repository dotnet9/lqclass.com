import { BindAll } from 'lodash-decorators';
import DataSource from 'store/dataSource';
import { notification } from 'antd';
import { url } from 'inspector';
import { getLocalesValue } from 'locale';
@BindAll()
export class Store extends DataSource {
    constructor() {
        super({
            // IdKey: "ID", 默认 ID
            // Target: "/api", 默认 /api
            Apis: {
                search: {
                    url: "/_dataprivilege/search",
                    method: "post"
                },
                details: {
                    // 支持 嵌套 参数 /user/{ID}/{AAA}/{BBB}
                    url: "/_dataprivilege/get",
                    method: "get"
                },
                insert: {
                    url: "/_dataprivilege/add",
                    method: "post"
                },
                update: {
                    url: "/_dataprivilege/edit",
                    method: "put"
                },
                delete: {
                    url: "/_dataprivilege/Delete",
                    method: "post"
                },
                import: {
                    url: "/_dataprivilege/import",
                    method: "post"
                },
                export: {
                    url: "/_dataprivilege/ExportExcel",
                    method: "post"
                },
                exportIds: {
                    url: "/_dataprivilege/ExportExcelByIds",
                    method: "post"
                },
                template: {
                    url: "/_dataprivilege/GetExcelTemplate",
                    method: "get"
                }
            }
        });
        this.DataSource.searchParams = { DpType: '0' }
    }

    /** 修改 */
    async onDetails(params) {
        console.log(params);
        const res = await this.Observable.Request.ajax({ ...this.options.Apis.details, body: params, headers: { 'Content-Type': null } }).toPromise();
        return res;
    }
    /**
       * 删除
       * @param params 
       */
    async onDelete(params) {
        this.PageState.tableLoading = true;
        try {
            const res = await this.Observable.onDelete({ ModelName: params.TableName, Type: params.DpType, Id: params.TargetId })
            notification.success({ message: getLocalesValue('tips.success.operation') });
            this.DataSource.selectedRowKeys = [];
            // 刷新数据
            this.onSearch(this.DataSource.searchParams);
            return res
        } catch (error) {
            this.PageState.tableLoading = false;
            notification.error({ message: getLocalesValue('tips.error.operation') });
        }
    }
}
export default new Store();

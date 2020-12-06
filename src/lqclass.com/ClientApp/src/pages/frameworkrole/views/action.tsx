import { Button, Divider, Dropdown, Menu, Modal, Popconfirm, Row } from 'antd';
import { DialogForm, Visible } from 'components/dataView';
import { DesError } from 'components/decorators';
import lodash from 'lodash';
import { observer } from 'mobx-react';
import * as React from 'react';
import { onAuthorizeActions } from 'store/system/authorize';
import Store from '../store';
import { InfoForm, InsertForm, UpdateForm, JurisdictionForm } from './forms';
import { FormattedMessage } from 'react-intl';
import { getLocalesTemplate } from 'locale';
/**
 * 动作事件
 */
export const ActionEvents = {
    /**
     * 导入
     */
    onImport() {
        Store.PageState.visiblePort = true;
    },
    /**
     * 导出
     */
    onExport() {
        Store.onExport()
    },
    /**
     * 批量导出
     */
    onExportIds() {
        Store.onExportIds()
    },
    /**
     * 删除
     * @param data 
     */
    onDelete(data) {
        Store.onDelete(data)
    },
    /**
    * 删除
    */
    onDeleteList() {
        const length = Store.DataSource.selectedRowKeys.length
        Modal.confirm({
            title: getLocalesTemplate('action.deleteConfirm', { text: length }),
            onOk: async () => {
                Store.onDelete(Store.DataSource.selectedRowKeys)
            },
            onCancel() { },
        });
    },
}
/**
 * 表格 所有 动作
 */
@DesError
@observer
class PageAction extends React.Component<any, any> {
    render() {
        const { selectedRowKeys } = Store.DataSource;
        const deletelength = selectedRowKeys.length;
        const disabled = deletelength < 1;
        return (
            <Row className="data-view-page-action">
                <Visible visible={onAuthorizeActions(Store, "insert")}>
                    <DialogForm
                        title={<FormattedMessage id="action.insert" />}
                        icon="plus"
                    >
                        <InsertForm />
                    </DialogForm>
                </Visible>
                <Visible visible={onAuthorizeActions(Store, "update")}>
                    <Divider type="vertical" />
                    <DialogForm
                        title={<FormattedMessage id="action.update" />}
                        icon="edit"
                        disabled={deletelength != 1}
                    >
                        <UpdateForm loadData={() => (lodash.find(selectedRowKeys))} />
                    </DialogForm>
                </Visible>
                <Visible visible={onAuthorizeActions(Store, "delete")}>
                    <Divider type="vertical" />
                    <Button icon="delete" onClick={ActionEvents.onDeleteList} disabled={disabled}><FormattedMessage id="action.delete" /></Button>
                </Visible>
                <Visible visible={onAuthorizeActions(Store, "import")}>
                    <Divider type="vertical" />
                    <Button icon="folder-add" onClick={ActionEvents.onImport}><FormattedMessage id="action.import" /></Button>
                </Visible>
                <Visible visible={onAuthorizeActions(Store, "export")}>
                    <Divider type="vertical" />
                    <Dropdown overlay={<Menu>
                        <Menu.Item>
                            <a onClick={ActionEvents.onExport}><FormattedMessage id="action.exportAll" /></a>
                        </Menu.Item>
                        <Menu.Item disabled={disabled}>
                            <a onClick={ActionEvents.onExportIds}><FormattedMessage id="action.exportSelect" /></a>
                        </Menu.Item>
                    </Menu>}>
                        <Button icon="download" ><FormattedMessage id="action.export" /></Button>
                    </Dropdown>
                </Visible>

            </Row>
        );
    }
}
/**
 * 表格 行 动作
 */
@DesError
@observer
class RowAction extends React.Component<{
    /** 数据详情 */
    data: any;
    [key: string]: any;
}, any> {
    render() {
        const { data } = this.props
        return (
            <Row className="data-view-row-action">
                <Visible visible={onAuthorizeActions(Store, "details")}>
                    <DialogForm
                        title={<FormattedMessage id="action.info" />}
                        showSubmit={false}
                        type="a"
                    >
                        <InfoForm loadData={data} />
                    </DialogForm>
                </Visible>
                <Visible visible={onAuthorizeActions(Store, "update")}>
                    <Divider type="vertical" />
                    <DialogForm
                        title={<FormattedMessage id="action.update" />}
                        type="a"
                    >
                        <UpdateForm loadData={data} />
                    </DialogForm>
                </Visible>
                <Visible visible={onAuthorizeActions(Store, "pages")}>
                    <Divider type="vertical" />
                    <DialogForm
                        title={<FormattedMessage id="action.privilege" />}
                    type="a"
                >
                        <JurisdictionForm loadData={data} />
                    </DialogForm>
                </Visible>
                <Visible visible={onAuthorizeActions(Store, "delete")}>
                    <Divider type="vertical" />
                    <Popconfirm title={<FormattedMessage id="action.deleteConfirm" values={{ text: '' }} />} onConfirm={() => { ActionEvents.onDelete(data) }} >
                        <a ><FormattedMessage id="action.delete" /></a>
                    </Popconfirm>
                </Visible>
            </Row>
        );
    }
}
export default {
    /**
     * 页面动作
     */
    pageAction: PageAction,
    /**
     * 数据行动作
     */
    rowAction: RowAction
}

/**
 * @author 冷 (https://github.com/LengYXin)
 * @email lengyingxin8966@gmail.com
 * @create date 2019-02-24 17:06:55
 * @modify date 2019-02-24 17:06:55
 * @desc [description]
 */
import * as React from 'react';
import lodash from "lodash";
import { Observable } from 'rxjs';
import { Spin, Icon, Tag } from 'antd';
interface ILoadingDataOptions {
    title?: string;
    value?: string;
    map?: (item) => any;
}
declare type linkageModels = (linkageModels?: any) => Observable<any[]> | any[] | Promise<any[]>;
export interface ILoadingDataProps {
    dataSource?: any;
    linkage?: string[];
    renderItem?: (items) => React.ReactNode;

    mapKey?: string;
    /**
     * 值转换
     * @memberof IValueFormatterOptions
     */
    valueFormatter?: (value) => any;
    /**
     * 值解析
     * @memberof IValueFormatterOptions
     */
    valueAnalysis?: (value) => any;
    [key: string]: any;
}
/**
 * 级联数据 装饰器
 * @param options 
 */
export function DesLoadingData(options: ILoadingDataOptions = {}) {
    options.title = lodash.get(options, 'title', 'Text');
    options.value = lodash.get(options, 'value', 'Value');
    options.map = lodash.get(options, 'map', (item) => {
        return {
            key: lodash.get(item, options.value),
            ...item,
            value: lodash.get(item, options.value),
            title: lodash.get(item, options.title),
        }
    });
    return function <T extends { new(...args: any[]): React.Component<ILoadingDataProps, any> }>(constructor: T) {
        return class LoadingData extends constructor {
            constructor(...args) {
                super(...args);
                this.state = {
                    spinning: false,
                    dataSource: [],
                    ...this.state,
                }
            }
            render() {
                if (!this.state.spinning) {
                    if (this.props.display) {
                        // 多选的
                        if (lodash.isArray(this.props.value)) {
                            let { value } = this.props;
                            if (this.props.mapKey) {
                                value = this.props.valueAnalysis ? this.props.valueAnalysis(value) : lodash.map(value, this.props.mapKey);
                            }
                            return value.map(val => {
                                const data = lodash.find(this.state.dataSource, ['key', val])
                                return data && <Tag color="geekblue" key={data.key}>{data.title}</Tag>
                            })
                        }
                        return <span>{lodash.get(lodash.find(this.state.dataSource, ["key", this.props.value]), "title")}</span>
                    }
                    // mapKey 数据转换
                    if (this.props.mapKey) {
                        const node = super.render();
                        let props = {
                            value: this.props.value,
                            onChange: (value, ...ags) => {
                                if (this.props.valueFormatter) {
                                    value = this.props.valueFormatter(value);
                                } else if (lodash.isArray(value)) {
                                    value = value.map(val => {
                                        if (lodash.isString(val)) {
                                            val = {
                                                [this.props.mapKey]: val
                                            }
                                        }
                                        return val
                                    })
                                }
                                this.props.onChange(value, ags);
                            }
                        };
                        if (props.value && lodash.isArray(props.value)) {
                            props.value = this.props.valueAnalysis ? this.props.valueAnalysis(props.value) : lodash.map(props.value, this.props.mapKey);
                        }
                        if (lodash.has(node, 'props.targetKeys')) {
                            lodash.set(props, 'targetKeys', props.value)
                        }
                        return React.cloneElement(node as JSX.Element, props)
                    }
                    return super.render();
                }
                return (
                    <Spin spinning={this.state.spinning} indicator={<Icon type="loading" spin />}>
                        <div>loading...</div>
                    </Spin>
                );
            }
            /**
             * 组件以卸载
             */
            __componentWillUnmount = false;
            /**
             * 联动 
             */
            __linkage;
            componentWillUnmount() {
                this.__componentWillUnmount = true;
                super.componentWillUnmount && super.componentWillUnmount()
            }
            componentDidMount() {
                if (this.props.dataSource) {
                    this.onLoadingData(this.props.dataSource);
                }
                super.componentDidMount && super.componentDidMount();
            }
            componentDidUpdate(prevProps, prevState, snapshot) {
                this.onlinkage();
                super.componentDidUpdate && super.componentDidUpdate(prevProps, prevState, snapshot);
            }
            /**
            * 加载数据
            * @param dataSource 
            */
            async  onLoadingData(dataSource) {
                let res = [];
                try {
                    if (!dataSource) {
                        this.setState({
                            dataSource: [],
                            spinning: false
                        })
                        return [];
                    }
                    if (this.state.spinning) return
                    this.setState({
                        spinning: true
                    })
                    // 值为 数组 
                    if (lodash.isFunction(dataSource)) {
                        if (this.props.linkage) {
                            this.__linkage = this.props.linkage;
                        }
                        return this.onLoadingData(dataSource(this.__linkage));
                    }
                    // 值为 数组 
                    if (lodash.isArray(dataSource)) {
                        res = dataSource;
                    }
                    // 值为 Promise
                    else if (dataSource instanceof Promise) {
                        res = await dataSource;
                    }
                    // 值为 Observable 
                    else if (dataSource instanceof Observable || dataSource.toPromise) {
                        res = await dataSource.toPromise();
                    }
                    // 转换 数据 为 渲染 格式
                    dataSource = res.map(item => {
                        return options.map(item);
                    })
                    // console.log("TCL: LoadingData -> onLoadingData -> dataSource", dataSource)
                } catch (error) {
                    dataSource = [];
                    console.error("Select 获取数据出错", error)
                }
                if (this.__componentWillUnmount) return
                this.setState({
                    dataSource,
                    spinning: false
                })
                return dataSource
            }
            /**
             * 联动模型
             */
            onlinkage() {
                try {
                    if (this.props.linkage && lodash.isFunction(this.props.dataSource)) {
                        const { getFieldValue, getFieldsValue, resetFields, setFields }: WrappedFormUtils = this.props.form;
                        const linkageValue = getFieldsValue(this.props.linkage);
                        // 联动数据无变化
                        if (lodash.isEqual(linkageValue, this.__linkage)) {
                            return
                        }
                        this.__linkage = linkageValue;
                        this.onLoadingData(this.props.dataSource(this.__linkage))
                    }
                } catch (error) {
                    console.error("TCL: LoadingData -> onlinkage -> error", error)
                }
            }
        }
    }
}
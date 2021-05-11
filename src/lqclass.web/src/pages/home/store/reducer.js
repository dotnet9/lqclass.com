import { fromJS } from 'immutable';

const defaultState = fromJS({
    topicList: [{
        id: 1,
        title: '大前端',
        imgUrl: 'https://dotnet9.com/wp-content/uploads/2020/05/d9ecb2d35379de206b2ada7ce9f7c8174eeaa53f.jpg'
    },{
        id: 2,
        title: '数据库',
        imgUrl: 'https://dotnet9.com/wp-content/uploads/2020/05/database.png'
    },{
        id: 3,
        title: 'C++',
        imgUrl: 'https://dotnet9.com/wp-content/uploads/2020/05/database.png'
    },{
        id: 4,
        title: 'C#',
        imgUrl: 'https://dotnet9.com/wp-content/uploads/2020/05/database.png'
    },{
        id: 5,
        title: 'JAVA',
        imgUrl: 'https://dotnet9.com/wp-content/uploads/2020/05/database.png'
    },{
        id: 6,
        title: 'JavaScript',
        imgUrl: 'https://dotnet9.com/wp-content/uploads/2020/05/database.png'
    },{
        id: 7,
        title: 'Vue',
        imgUrl: 'https://dotnet9.com/wp-content/uploads/2020/05/database.png'
    },{
        id: 8,
        title: 'React',
        imgUrl: 'https://dotnet9.com/wp-content/uploads/2020/05/database.png'
    },{
        id: 9,
        title: 'Angular.js',
        imgUrl: 'https://dotnet9.com/wp-content/uploads/2020/05/database.png'
    }],
    articleList: [{
        id: 1,
        title: '项目多也别傻做——享元模式',
        desc: '继上一篇“世界需要和平——中介者模式”后，本文继续讲解《大话设计模式》第26章“项目多也别傻做——享元模式”。喜欢本书请到各大商城购买原书，支持正版。',
        imgUrl: 'https://dotnet9.com/wp-content/uploads/2020/05/d9ecb2d35379de206b2ada7ce9f7c8174eeaa53f.jpg'
    },{
        id: 2,
        title: '项目多也别傻做——享元模式',
        desc: '继上一篇“世界需要和平——中介者模式”后，本文继续讲解《大话设计模式》第26章“项目多也别傻做——享元模式”。喜欢本书请到各大商城购买原书，支持正版。',
        imgUrl: 'https://dotnet9.com/wp-content/uploads/2020/05/database.png'
    },{
        id: 3,
        title: '项目多也别傻做——享元模式++',
        desc: '继上一篇“世界需要和平——中介者模式”后，本文继续讲解《大话设计模式》第26章“项目多也别傻做——享元模式”。喜欢本书请到各大商城购买原书，支持正版。',
        imgUrl: 'https://dotnet9.com/wp-content/uploads/2020/05/database.png'
    },{
        id: 4,
        title: '项目多也别傻做——享元模式#',
        desc: '继上一篇“世界需要和平——中介者模式”后，本文继续讲解《大话设计模式》第26章“项目多也别傻做——享元模式”。喜欢本书请到各大商城购买原书，支持正版。',
        imgUrl: 'https://dotnet9.com/wp-content/uploads/2020/05/database.png'
    },{
        id: 5,
        title: '项目多也别傻做——享元模式',
        desc: '继上一篇“世界需要和平——中介者模式”后，本文继续讲解《大话设计模式》第26章“项目多也别傻做——享元模式”。喜欢本书请到各大商城购买原书，支持正版。',
        imgUrl: 'https://dotnet9.com/wp-content/uploads/2020/05/database.png'
    },{
        id: 6,
        title: '项目多也别傻做——享元模式',
        desc: '继上一篇“世界需要和平——中介者模式”后，本文继续讲解《大话设计模式》第26章“项目多也别傻做——享元模式”。喜欢本书请到各大商城购买原书，支持正版。',
        imgUrl: 'https://dotnet9.com/wp-content/uploads/2020/05/database.png'
    },{
        id: 7,
        title: '项目多也别傻做——享元模式',
        desc: '继上一篇“世界需要和平——中介者模式”后，本文继续讲解《大话设计模式》第26章“项目多也别傻做——享元模式”。喜欢本书请到各大商城购买原书，支持正版。',
        imgUrl: 'https://dotnet9.com/wp-content/uploads/2020/05/database.png'
    },{
        id: 8,
        title: '项目多也别傻做——享元模式',
        desc: '继上一篇“世界需要和平——中介者模式”后，本文继续讲解《大话设计模式》第26章“项目多也别傻做——享元模式”。喜欢本书请到各大商城购买原书，支持正版。',
        imgUrl: 'https://dotnet9.com/wp-content/uploads/2020/05/database.png'
    },{
        id: 9,
        title: '项目多也别傻做——享元模式.js',
        desc: '继上一篇“世界需要和平——中介者模式”后，本文继续讲解《大话设计模式》第26章“项目多也别傻做——享元模式”。喜欢本书请到各大商城购买原书，支持正版。',
        imgUrl: 'https://dotnet9.com/wp-content/uploads/2020/05/database.png'
    }],
    recommendList: [{
        id: 1,
        imgUrl: 'https://dotnet9.com/wp-content/uploads/2020/05/d9ecb2d35379de206b2ada7ce9f7c8174eeaa53f.jpg'
    },{
        id: 2,
        imgUrl: 'https://dotnet9.com/wp-content/uploads/2020/05/database.png'
    },{
        id: 3,
        imgUrl: 'https://dotnet9.com/wp-content/uploads/2020/05/database.png'
    },{
        id: 4,
        imgUrl: 'https://dotnet9.com/wp-content/uploads/2020/05/database.png'
    }]
});

export default (state = defaultState, action) => {
    switch(action.type) {
        default:            
            return state;
    }
}
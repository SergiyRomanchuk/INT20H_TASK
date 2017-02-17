var vk = {
    data: {},
    api: "//vk.com/js/api/openapi.js",
    appID: 5870848,
    appPermissions: 'W7k78MIIZkGh3hGwmevP',
    init: function () {
        $.js(vk.api);
        window.vkAsyncInit = function () {
            VK.init({ apiId: vk.appID });
            load();
        }

        function load() {
            VK.Auth.login(authInfo, vk.appPermissions);

            function authInfo(response) {
                if (response.session) { // Авторизация успешна
                    vk.data.user = response.session.user;
                    vk.getFriends();
                } else alert("Авторизоваться не удалось!");
            }
        }
    },
    getFriends: function () {
        VK.Api.call('friends.get', { fields: ['uid', 'first_name', 'last_name'], order: 'name' }, function (r) {
            if (r.response) {
                r = r.response;
                var ol = $('#clientApi').add('ol');
                for (var i = 0; i < r.length; ++i) {
                    var li = ol.add('li').html(r[i].first_name + ' ' + r[i].last_name + ' (' + r[i].uid + ')')
                }
            } else alert("Не удалось получить список ваших друзей");
        })
    }
}

$.ready(vk.init);
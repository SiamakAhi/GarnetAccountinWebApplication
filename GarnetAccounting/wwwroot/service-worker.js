
const CACHE_NAME = 'garnet-cache-v3';
const urlsToCache = [
    '/', // صفحه اصلی
    '/Home/Index', // آدرس شروع
    '/vendor/css/rtl/core.css', 
    '/css/demo.css', 
    '/css/font.css', 
    '/css/login.css', 
    '/js/scripts.js', 
    '/js/App/AccCoding.js', 
    '/js/App/AppSaleReport.js', 
    '/icons/icon192.png', 
    '/icons/icon512.png'  
];


self.addEventListener('install', event => {
    event.waitUntil(
        caches.open(CACHE_NAME)
            .then(cache => {
                console.log('Opened cache');
                return cache.addAll(urlsToCache);
            })
    );
});


self.addEventListener('fetch', event => {
    event.respondWith(
        caches.match(event.request)
            .then(response => {
                if (response) {
                    return response;
                }
                return fetch(event.request);
            })
    );
});

self.addEventListener('activate', event => {
    const cacheWhitelist = [CACHE_NAME];
    event.waitUntil(
        caches.keys().then(cacheNames => {
            return Promise.all(
                cacheNames.map(cacheName => {
                    if (!cacheWhitelist.includes(cacheName)) {
                        return caches.delete(cacheName);
                    }
                })
            );
        })
    );
});


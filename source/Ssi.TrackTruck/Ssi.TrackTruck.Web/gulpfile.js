(function () {
    var destFolder = 'bin.client';
    var minify = false;
    var gulp = require('gulp'),
        concat = require('gulp-concat'),
        uglify = require('gulp-uglify'),
        expect = require('gulp-expect-file')
    ;
    var cleanCSS = require('gulp-clean-css');
    var templateCache = require('gulp-angular-templatecache');
    var merge = require('merge-stream');

    var jsLibs = [
            { src: 'angular/angular.js', min: 'angular/angular.min.js' },
            { src: 'angular-route/angular-route.js', min: 'angular-route/angular-route.min.js' },
            { src: 'angular-animate/angular-animate.js', min: 'angular-animate/angular-animate.min.js' },
            { src: 'angular-bootstrap/ui-bootstrap-tpls.js', min: 'angular-bootstrap/ui-bootstrap-tpls.js' },
            { src: 'lodash/dist/lodash.js', min: 'lodash/dist/lodash.min.js' },
            { src: 'angular-tablesort/js/angular-tablesort.js', min: 'angular-tablesort/js/angular-tablesort.js' },
            { src: 'ng-tags-input/ng-tags-input.js', min: 'ng-tags-input/ng-tags-input.min.js' },
            { src: 'moment/moment.js', min: 'moment/min/moment.min.js' }
    ];

    function concatAndMinify(src, outputFile) {
        var stream = gulp.src(src)
            .pipe(concat(outputFile, { newLine: '\r\n\r\n' }));

        if (minify) {
            stream = stream.pipe(uglify());
        }

        return stream.pipe(gulp.dest(destFolder));
    }

    function concateTemplates(src, outputFile, ngModule, root) {
        return gulp.src(src)
            .pipe(templateCache(outputFile, {
                module: ngModule,
                standalone: false,
                root: root
            }))
            .pipe(gulp.dest(destFolder));
    }

    gulp.task('_build-js-app', function () {
        return merge(concatAndMinify('Scripts/App/**/*.js', 'app.js')
            , concateTemplates('Scripts/App/**/*.html', 'app.templates.js', 'trackTruck'));
    });

    gulp.task('_build-js-signin', function () {
        return concatAndMinify('Scripts/SignIn/*.js', 'signin.js');
    });

    gulp.task('_build-js-utils', function () {
        return merge(concatAndMinify('Scripts/Util/*.js', 'util.js')
            , concateTemplates('Scripts/Util/*.html', 'util.templates.js', 'utilModule', 'Util/'));
    });

    gulp.task('_build-js-libs', function () {
        var prop = minify ? 'min' : 'src';
        var libs = jsLibs.map(function (file) {
            return 'bower_components/' + file[prop];
        });

        return gulp.src(libs)
            .pipe(expect(libs))
            .pipe(concat('lib.js'))
            .pipe(gulp.dest(destFolder));
    });

    gulp.task('build-js', ['_build-js-utils', '_build-js-libs', '_build-js-signin', '_build-js-app']);

    gulp.task('build-css', function () {
        var files = [
            'Content/css/bootstrap-superhero.min.css'
            , 'Content/css/bootstrap-superhero-override.css'
            , 'Content/css/font-awesome.min.css'
            , 'Content/css/tablesort.css'
            , 'Content/css/animate.css'
            , 'Content/css/ng-tags-input.css'
            , 'Content/css/ng-tags-input.bootstrap.css'
            , 'Content/css/track-truck.css'
        ];

        gulp.src('Content/fonts/*')
            .pipe(gulp.dest(destFolder + '/fonts'));

        var stream = gulp.src(files)
            .pipe(expect(files))
            .pipe(concat('all.css'));

        if (minify) {
            stream = stream.pipe(cleanCSS());
        }

        stream.pipe(gulp.dest(destFolder + '/css'));

        return stream;
    });

    gulp.task('watch', ['build-js', 'build-css'], function () {
        function changed(event) {
            console.log('File ' + event.path + ' was ' + event.type + ', running tasks...');
        }

        [
            gulp.watch('Content/css/*.css', ['build-css']),
            gulp.watch('Scripts/App/**/*', ['_build-js-app']),
            gulp.watch('Scripts/Util/**/*', ['_build-js-utils']),
            gulp.watch('Scripts/SignIn/**/*', ['_build-js-signin'])
        ].forEach(function (watcher) {
            watcher.on('change', changed);
        });
    });

    gulp.task('default', ['build-js', 'build-css']);
})();


(function () {
    var minify = false;
    var gulp = require('gulp'),
        concat = require('gulp-concat'),
        uglify = require('gulp-uglify'),
        expect = require('gulp-expect-file')
    ;

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

        return stream.pipe(gulp.dest('bin.client'));
    }

    gulp.task('_build-app', function() {
        return concatAndMinify('Scripts/App/**/*.js', 'app.js');
    });

    gulp.task('_build-signin', function () {
        return concatAndMinify('Scripts/SignIn/*.js', 'signin.js');
    });

    gulp.task('_build-utils', function () {
        return concatAndMinify('Scripts/Util/*.js', 'util.js');
    });

    gulp.task('_build-libs', function () {
        var prop = minify ? 'min' : 'src';
        var libs = jsLibs.map(function(file) {
            return 'bower_components/' + file[prop];
        });

        return gulp.src(libs)
            .pipe(expect(libs))
            .pipe(concat('lib.js'))
            .pipe(gulp.dest('bin.client'));
    });

    gulp.task('build-js', ['_build-utils', '_build-libs', '_build-signin', '_build-app']);
})();


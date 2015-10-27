var gulp = require('gulp');  
var mainBowerFiles = require('main-bower-files');
var bower = require('gulp-bower');  
var uglify = require('gulp-uglify');
var mincss = require('gulp-cssmin');
var concat = require('gulp-concat');
var ignore = require('gulp-ignore');
var del = require('del');

var project = require('./project.json');  
var lib = project.webroot + '/dist'; 
var content = project.webroot + '/Content';

/* LIMPIO LA CARPETA DE DESTINO */
gulp.task('clean',function(done){
	del(lib, done);
});

/* ME ASEGURO DE QUE TODOS LOS PAQUETES DE bower.json ESTAN INSTALADOS */
gulp.task('bower:install', ['clean'], function () {  
    return bower();
});

gulp.task('showfiles', ['bower:install'], function () {
    console.log(mainBowerFiles());
    return;
});

/* MINIFICO TODOS LOS PAQUETES */
gulp.task('minifyJs', ['bower:install'], function () {  
    return gulp.src(mainBowerFiles())
        .pipe(ignore.exclude([ "**/*.css" ]))
      .pipe(uglify())
      .pipe(gulp.dest(lib+'/js'));
});

/* MINIFICO JS PARTICULARES CREADOS POR MI */
gulp.task('minifyJsContent', ['minifyJs'], function () {  
    return gulp.src(content+'/js/*.js')
      .pipe(uglify())
      .pipe(gulp.dest(lib+'/js'));
});

/* MINIFICO CSS PARTICULARES CREADOS POR MI */
gulp.task('minifyCssContent', ['minifyJsContent'], function () {  
    return gulp.src(content+'/css/site.css')
        .pipe(concat('front.css'))
      .pipe(mincss())
      .pipe(gulp.dest(lib+'/css/bundle/'));
});

/* BUNDLE PARA FRONT */
gulp.task('bundleFront', ['minifyCssContent'], function () {  
    return gulp.src([lib+'/js/layzr.js',lib+'/js/material.min.js',lib+'/js/layout.js'])
      .pipe(concat('front.js'))
      .pipe(gulp.dest(lib+'/js/bundle/'));
});

gulp.task('default', ['bundleFront'], function () {  
    return;
});
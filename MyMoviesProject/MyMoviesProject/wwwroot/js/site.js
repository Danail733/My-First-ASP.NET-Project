var ratingStars = [...document.getElementsByClassName("rating__star")];
var ratingResult = document.querySelector(".rating__result");

//printRatingResult(ratingResult);

function executeRating(stars, result) {
    const starClassActive = "rating__star fas fa-star";
    const starClassUnactive = "rating__star far fa-star";
    const starsLength = stars.length;
    let i;
    stars.map((star) => {
        star.onclick = () => {
            i = stars.indexOf(star);

            if (star.className.indexOf(starClassUnactive) !== -1) {
                printRatingResult(result, i + 1);
                for (i; i >= 0; --i) stars[i].className = starClassActive;
            } else {
                printRatingResult(result, i);
                for (i; i < starsLength; ++i) stars[i].className = starClassUnactive;
            }
        };
    });
}

function printRatingResult(result, num = 0) {
    result.textContent = `${num}/5`;
}

executeRating(ratingStars, ratingResult);

$('.rating').click(function (e) {
    let ratingValue = e.target.id;
    let movieId = e.target.parentNode.id;
    $.post('/api/movies', {
        ratingValue: ratingValue,
        movieId: movieId
    }).done(function (data) {
        if (data !== null) {
            alert(`You rated the movie with ${ratingValue}!`);
            $.get('/api/movies', {
                movieId: movieId,
            }).done(function (data) {
                document.querySelector('#averageRating').textContent = `(${data.toFixed(2)})`;
            })
        }
    });
})

function fillStars(stars, result, data) {
    const starClassActive = "rating__star fas fa-star";

    printRatingResult(result, data);
    for (let i = 0; i < data; i++) {
        stars[i].className = starClassActive;
        }
}


window.onload = function () {
    const dataRatingResult = document.querySelector(".rating__result");
    const dataRatingStars = [...document.getElementsByClassName("rating__star")];
    let movieId = document.querySelector('.rating').id;
    $.get('/api/users', {
        movieId: movieId
    }).done(function (data) {
        fillStars(dataRatingStars, dataRatingResult, data);
    })
    };

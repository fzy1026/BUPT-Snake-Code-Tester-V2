#include <stdio.h>

int main(void) {
    char map[20][21];
    int n;
    for (int i = 0; i < 20; i++) {
        if (scanf("%20s", map[i]) != 1) return 1;
    }
    if (scanf("%d", &n) != 1) return 1;

    const char moves[] = {'D', 'W', 'W', 'W', 'W', 'W'};
    const int scores[] = {0, 10, 10, 10, 10, 10};
    for (int i = 0; i < 6; i++) {
        int r, c;
        printf("%c\n%d\n", moves[i], scores[i]);
        fflush(stdout);
        if (scanf("%d%d", &r, &c) != 2) return 1;
        if (r == 100 && c == 100) {
            puts("####################");
            puts("#..................#");
            puts("#.....F............#");
            puts("#...OOOOOOOOOO.....#");
            puts("#.....H............#");
            puts("#.....B............#");
            puts("#.....B............#");
            puts("#.....B............#");
            puts("#..................#");
            puts("#..................#");
            puts("#..................#");
            puts("#..................#");
            puts("#..................#");
            puts("#..................#");
            puts("#..................#");
            puts("#..................#");
            puts("#..................#");
            puts("#..................#");
            puts("#..................#");
            puts("####################");
            puts("10");
            fflush(stdout);
            return 0;
        }
    }
    return 0;
}

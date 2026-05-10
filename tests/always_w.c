#include <stdio.h>

int main(void) {
    char map[20][21];
    int n;
    for (int i = 0; i < 20; i++) {
        if (scanf("%20s", map[i]) != 1) return 0;
    }
    if (scanf("%d", &n) != 1) return 0;
    while (1) {
        int r, c;
        printf("W\n0\n");
        fflush(stdout);
        if (scanf("%d%d", &r, &c) != 2) return 0;
        if (r == 100 && c == 100) {
            for (int i = 0; i < 20; i++) puts(map[i]);
            puts("0");
            fflush(stdout);
            return 0;
        }
    }
}

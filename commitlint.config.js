/** @type {import('@commitlint/types').UserConfig} */
module.exports = {
  extends: ['@commitlint/config-conventional'],
  rules: {
    // Enforce allowed types
    'type-enum': [
      2,
      'always',
      ['feat', 'fix', 'docs', 'style', 'refactor', 'test', 'chore', 'perf', 'ci'],
    ],
    // Enforce allowed scopes (warn only — not blocking)
    'scope-enum': [
      1,
      'always',
      [
        'auth', 'albums', 'tracks', 'users', 'feed',
        'spotify', 'api', 'web', 'infra', 'deps',
      ],
    ],
    // Subject line max length
    'subject-max-length': [2, 'always', 100],
    // No period at end of subject
    'subject-full-stop': [2, 'never', '.'],
    // Lowercase subject
    'subject-case': [2, 'never', ['sentence-case', 'start-case', 'pascal-case', 'upper-case']],
    // Body line max length
    'body-max-line-length': [2, 'always', 200],
  },
};

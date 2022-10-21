namespace EG.LinkedInNet.Models;

public enum AssetType
{
    /// <summary>
    ///     A piece of written work published in a print or electronic medium. It is a type of document which refers to a
    ///     specific topic, forming an independent part of a publication such as book, newspaper or web platform. Ex. Blog
    ///     post.
    /// </summary>
    ARTICLE,

    /// <summary>
    ///     Used to evaluate a learner's skill and knowledge.
    /// </summary>
    ASSESSMENT,

    /// <summary>
    ///     Audio-only content with no video elements, like audio books and podcasts.
    /// </summary>
    AUDIO,

    /// <summary>
    ///     Includes full length books presented in any form, text, audiobook, or otherwise. Does not include book summaries,
    ///     reviews, excerpts, or other incomplete versions of a book.
    /// </summary>
    BOOK,

    /// <summary>
    ///     The learning asset is a chapter.
    /// </summary>
    CHAPTER,

    /// <summary>
    ///     The learning asset is a course.
    /// </summary>
    COURSE,

    /// <summary>
    ///     A document refers to a medium of record for decisions, transactions, plan etc. Ex. Wiki page, PDF, etc.
    /// </summary>
    DOCUMENT,

    /// <summary>
    ///     An event is an entity that can be scheduled to happen at a particular time where the learning content is
    ///     synchronously offered (in-person or online) to the learners. Ex. workshops, seminars, tech talks, trainings.
    /// </summary>
    EVENT,

    /// <summary>
    ///     A learning collection is a non-sequential collection of learning assets generally centered around a skill or
    ///     concept.
    /// </summary>
    LEARNING_COLLECTION,

    /// <summary>
    ///     A learning path is a guided sequential collection of courses and other content that is designed to teach a skill or
    ///     set of skills. Generally much larger in scope than an individual course.
    /// </summary>
    LEARNING_PATH,

    /// <summary>
    ///     The learning asset is a video.
    /// </summary>
    VIDEO
}
